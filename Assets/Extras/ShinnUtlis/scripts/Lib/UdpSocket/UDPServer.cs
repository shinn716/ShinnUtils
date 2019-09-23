// Author : Shinn
// Date : 20190905
// Reference :  http://kimdicks.blogspot.com/2017/11/unityudp.html
//
// server = new UDPServer();
// t = new Thread(new ThreadStart(server.ReceiveData));
// t.Start();
// server.callback += getres;
// server.callback -= getres;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Shinn.Common
{
    public class UDPServer
    {
        private IPEndPoint ipEndPoint;
        private UdpClient udpClient;
        private byte[] receiveByte;
        private string receiveData = string.Empty;
        private bool start = true;
        public delegate void Callback();
        public Callback callback;

        /// <summary>
        /// Default ip = "127.0.0.1", port = 10000
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public UDPServer(string ip = "127.0.0.1", int port = 10000)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            udpClient = new UdpClient(ipEndPoint.Port);
            receiveByte = new byte[1024];
            start = true;
        }

        /// <summary>
        /// Start receive.
        /// </summary>
        public void ReceiveData()
        {
            while (start)
            {
                receiveByte = udpClient.Receive(ref ipEndPoint);
                receiveData = Encoding.UTF8.GetString(receiveByte);
                callback?.Invoke();
            }
        }

        /// <summary>
        /// Call back.
        /// </summary>
        /// <returns></returns>
        public string CallbackEvent()
        {
            return receiveData;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (udpClient != null)
            {
                start = false;
                ((System.IDisposable)udpClient).Dispose();
                udpClient.Close();
                udpClient = null;
            }
        }

    }
}
