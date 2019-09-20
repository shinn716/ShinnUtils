// Author : Shinn
// Date : 20190905
// Reference :  http://kimdicks.blogspot.com/2017/11/unityudp.html
//              https://blog.csdn.net/NippyLi/article/details/79123609
//              https://stackoverflow.com/questions/8321271/c-sharp-slow-socket-speed            

using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Shinn.Common
{
    public class UDPServer
    {
        public string Ip { get; set; } = IPAddress.Any.ToString();
        public int Port { get; set; } = 10000;
        public string Output { get; set; }

        private const int SendBufferLength = 524266;

        private Socket socket;
        private EndPoint clientEnd;
        private IPEndPoint ipEnd;
        private Thread connectThread;

        private string recvStr;
        private string sendStr;
        private byte[] recvData = new byte[SendBufferLength];
        private byte[] sendData = new byte[SendBufferLength];
        private int recvLen;
        
        public UDPServer()
        {
            Debug.Log("UDPServer");
            ipEnd = new IPEndPoint(ToInt(Ip), Port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(ipEnd);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            clientEnd = (EndPoint)sender;
            Debug.Log("waiting for UDP dgram");
        }

        public void Init()
        {
            Debug.Log("Init");
            connectThread = new Thread(new ThreadStart(SocketReceive));
            connectThread.Start();
        }

        public void Dispose()
        {
            Debug.Log("Dispose");
            if (connectThread != null)
            {
                connectThread.Interrupt();
                connectThread.Abort();
            }
            if (socket != null)
                socket.Close();
        }

        private void SocketSend(string sendStr)
        {
            sendData = new byte[SendBufferLength];
            sendData = Encoding.ASCII.GetBytes(sendStr);
            socket.SendTo(sendData, sendData.Length, SocketFlags.None, clientEnd);
        }

        private void SocketReceive()
        {
            while (true)
            {
                recvData = new byte[SendBufferLength];
                recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
                //Debug.Log("message from: " + clientEnd.ToString());
                recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
                //Debug.Log(recvStr);
                sendStr = "From Server: " + recvStr;
                //SocketSend(sendStr);
                Output = sendStr;
            }
        }

        private long ToInt(string addr)
        {
            // careful of sign extension: convert to uint first;
            // unsigned NetworkToHostOrder ought to be provided.
            return (long)(uint)IPAddress.NetworkToHostOrder(
                 (int)IPAddress.Parse(addr).Address);
        }
    }
}
