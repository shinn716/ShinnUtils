// Author : Shinn
// Date : 20190905
// Reference : http://kimdicks.blogspot.com/2017/11/unityudp.html
// 

using UnityEngine;
using System.Net;
using System.Net.Sockets;

namespace Shinn.Commom
{
    public class UDPClient
    {
        private IPEndPoint ipEndPoint;
        private UdpClient udpClient;
        private byte[] sendByte;

        /// <summary>
        /// No need to Init
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public UDPClient(string ip = "127.0.0.1", int port = 10000)
        {
            //Debug.Log("UDPClient");
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            udpClient = new UdpClient();
        }

        /// <summary>
        /// Send Data -> string
        /// </summary>
        /// <param name="tempData"></param>
        public void SendData(string tempData)
        {
            sendByte = System.Text.Encoding.UTF8.GetBytes(tempData);
            udpClient.Send(sendByte, sendByte.Length, ipEndPoint);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            udpClient.Dispose();
            udpClient.Close();
            udpClient = null;
        }
    }
}