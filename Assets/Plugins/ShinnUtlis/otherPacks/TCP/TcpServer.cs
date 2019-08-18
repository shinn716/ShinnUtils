using System;
using System.Collections;
using System.Collections.Generic;
//引入庫
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class TcpServer : MonoBehaviour
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }

    //以下默認都是私有的成員
    public int port = 5566;

    Socket serverSocket; //伺服器端socket
    Socket clientSocket; //用戶端socket
    IPEndPoint ipEnd; //偵聽埠
    string recvStr; //接收的字串
    string sendStr; //發送的字串
    byte[] recvData = new byte[1024]; //接收的資料，必須為位元組
    byte[] sendData = new byte[1024]; //發送的資料，必須為位元組
    int recvLen; //接收的資料長度
    Thread connectThread; //連接執行緒

    public StringEvent onReceived;
    private Queue<string> receivedQueue = new Queue<string>();

    //初始化
    void InitSocket()
    {
        //定義偵聽埠,偵聽任何IP
        ipEnd = new IPEndPoint(IPAddress.Any, port);
        //定義通訊端類型,在主執行緒中定義
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //連接
        serverSocket.Bind(ipEnd);
        //開始偵聽,最大10個連接
        serverSocket.Listen(1);
        //開啟一個執行緒連接，必須的，否則主執行緒卡死
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }
    //連接
    void SocketConnet()
    {
        if (clientSocket != null)
            clientSocket.Close();
        //控制台輸出偵聽狀態
        print("Waiting for a client");
        //一旦接受連接，創建一個用戶端
        clientSocket = serverSocket.Accept();
        //獲取用戶端的IP和埠
        IPEndPoint ipEndClient = (IPEndPoint)clientSocket.RemoteEndPoint;
        //輸出用戶端的IP和埠
        print("Connect with " + ipEndClient.Address.ToString() + ":" + ipEndClient.Port.ToString());
        //連接成功則發送資料
        sendStr = "Welcome to my server";
        SocketSend(sendStr);
    }
    void SocketSend(string sendStr)
    {
        //清空發送緩存
        sendData = new byte[1024];
        //資料類型轉換
        sendData = Encoding.ASCII.GetBytes(sendStr);
        //發送
        clientSocket.Send(sendData, sendData.Length, SocketFlags.None);
    }
    //伺服器接收
    void SocketReceive()
    {
        //連接
        SocketConnet();
        //進入接收迴圈
        while (true)
        {
            //對data清零
            recvData = new byte[1024];
            //獲取收到的資料的長度
            recvLen = clientSocket.Receive(recvData);
            //如果收到的資料長度為0，則重連並進入下一個迴圈
            if (recvLen == 0)
            {
                SocketConnet();
                continue;
            }
            //輸出接收到的資料
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            receivedQueue.Enqueue(recvStr);
        }
    }
    //連接關閉
    void SocketQuit()
    {
        //先關閉用戶端
        if (clientSocket != null)
            clientSocket.Close();
        //再關閉執行緒
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最後關閉伺服器
        serverSocket.Close();
        print("diconnect");
    }
    // Use this for initialization
    void Start()
    {
        InitSocket(); //在這裡初始化server
    }

    private void Update()
    {
        if (receivedQueue.Count > 0)
        {
            var dequeue = receivedQueue.Dequeue();

            onReceived.Invoke(dequeue);
        }
    }

    void OnApplicationQuit()
    {
        SocketQuit();
    }


}


