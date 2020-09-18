// Author : Shinn
// Date : 20190905
// Reference : http://kimdicks.blogspot.com/2017/11/unityudp.html
// 

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Shinn.Common
{
    public class UDPClient
    {
        private IPEndPoint ipEndPoint;
        private UdpClient udpClient;
        private byte[] sendByte;

        /// <summary>
        /// Default ip = "127.0.0.1", port = 10000
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
        public void SendData_String(string tempData)
        {
            sendByte = Encoding.UTF8.GetBytes(tempData);
            udpClient.Send(sendByte, sendByte.Length, ipEndPoint);
        }

        /// <summary>
        /// Send Data -> string
        /// </summary>
        /// <param name="tempData"></param>
        public void SendData_Byte(string tempData)
        {
            var bytes = Encoding.UTF8.GetBytes(tempData);
            sendByte = bytes;
            udpClient.Send(sendByte, sendByte.Length, ipEndPoint);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (udpClient != null)
            {
                udpClient.Dispose();
                udpClient.Close();
                udpClient = null;
            }
        }

    }
}