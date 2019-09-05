// Author : Shinn
// Date : 20190905
// Reference : http://kimdicks.blogspot.com/2017/11/unityudp.html
//

using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Shinn.Commom
{
    public class UDPServer
    {
        public string Output { get; set; }

        private IPEndPoint ipEndPoint;
        private UdpClient udpClient;
        private Thread receiveThread;

        private byte[] receiveByte;
        private string receiveData = string.Empty;
        private bool threadStart;

        public UDPServer(string ip = "0.0.0.0", int port = 10000)
        {
            //Debug.Log("UDPServer");
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            udpClient = new UdpClient(ipEndPoint.Port);
        }

        /// <summary>
        /// Init thread.
        /// </summary>
        public void Init()
        {
            //Debug.Log("Init");
            receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();
            threadStart = true;
        }

        /// <summary>
        /// Stop thread.
        /// </summary>
        public void Dispose()
        {
            //Debug.Log("Dispose");
            threadStart = false;
            udpClient.Dispose();
            udpClient.Close();
            udpClient = null;

            receiveThread.Join();
            receiveThread.Abort();
            receiveThread = null;
        }

        /// <summary>
        /// Receive data.
        /// </summary>
        private void ReceiveData()
        {
            while (threadStart)
            {
                receiveByte = udpClient.Receive(ref ipEndPoint);
                receiveData = System.Text.Encoding.UTF8.GetString(receiveByte);
                Output = receiveData;
                Debug.Log("Receive: " + receiveData);
            }
        }
    }
}
