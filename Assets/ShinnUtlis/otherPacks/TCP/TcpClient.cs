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
using UnityEngine.UI;

public class TcpClient : MonoBehaviour
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }

    public string serverIP = "127.0.0.1";
    public int serverPort = 5566;

    string editString = "hello wolrd"; //編輯方塊文字
    Socket serverSocket;               //伺服器端socket
    IPAddress ip;                      //主機ip
    IPEndPoint ipEnd;
    string recvStr;                    //接收的字串
    string sendStr;                    //發送的字串
    byte[] recvData = new byte[1024];  //接收的資料，必須為位元組
    byte[] sendData = new byte[1024];  //發送的資料，必須為位元組
    int recvLen;                       //接收的資料長度
    Thread connectThread;              //連接執行緒

    public StringEvent onReceived;
    private Queue<string> receivedQueue = new Queue<string>();

    //初始化
    void InitSocket()
    {
        ip = IPAddress.Parse(serverIP); //可以是局域網或互聯網ip，此處是本機
        ipEnd = new IPEndPoint(ip, serverPort);
        //開啟一個執行緒連接，必須的，否則主執行緒卡死
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void SocketConnet()
    {
        if (serverSocket != null)
            serverSocket.Close();
        //定義通訊端類型,必須在子執行緒中定義
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        print("ready to connect");
        //連接
        serverSocket.Connect(ipEnd);
        //輸出初次連接收到的字串
        recvLen = serverSocket.Receive(recvData);
        recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
        print(recvStr);
    }

    void SocketSend(string sendStr)
    {
        //清空發送緩存
        sendData = new byte[1024];
        //資料類型轉換
        sendData = Encoding.ASCII.GetBytes(sendStr);
        //發送
        serverSocket.Send(sendData, sendData.Length, SocketFlags.None);
    }

    void SocketReceive()
    {
        SocketConnet();
        //不斷接收伺服器發來的資料
        while (true)
        {
            recvData = new byte[1024];
            recvLen = serverSocket.Receive(recvData);
            if (recvLen == 0)
            {
                SocketConnet();
                continue;
            }
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            onReceived.Invoke(recvStr);
        }
    }

    void SocketQuit()
    {
        //關閉執行緒
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最後關閉伺服器
        if (serverSocket != null)
            serverSocket.Close();
        print("diconnect");
    }
    
    void Start()
    {
        InitSocket();
    }

    void OnGUI()
    {
        editString = GUI.TextField(new Rect(10, 10, 100, 20), editString);
        if (GUI.Button(new Rect(10, 30, 60, 20), "send"))
            SocketSend(editString);
    }

    private void Update()
    {
        if (receivedQueue.Count > 0)
        {
            var dequeue = receivedQueue.Dequeue();

            onReceived.Invoke(dequeue);
        }
    }

    //程式退出則關閉連接
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}
