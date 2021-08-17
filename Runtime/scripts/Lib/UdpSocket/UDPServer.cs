//
// UDPServer - Unity UDP Socket
//
// Copyright (C) 2021 John Tsai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Shinn.Common
{
    public class UDPServer
    {
        public delegate void Callback();
        public Callback callback;

        private IPEndPoint ipEndPoint;
        private UdpClient udpClient;
        private byte[] receiveByte;
        private string receiveData = string.Empty;
        private Thread thread;
        
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
            
            thread = new Thread(new ThreadStart(ReceiveData));
            thread.Start();

            Debug.Log($"Init UDPServer {ip}/{port}");
        }

        /// <summary>
        /// Call back.
        /// </summary>
        /// <returns></returns>
        public string GetReceiveData()
        {
            return receiveData;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (udpClient != null)
                udpClient.Close();

            if (thread != null)
                thread.Abort();
        }
        
        private void ReceiveData()
        {
            while (true)
            {
                receiveByte = udpClient.Receive(ref ipEndPoint);
                receiveData = Encoding.UTF8.GetString(receiveByte);

                //if (callback != null)                 // net 2.0
                //    callback.Invoke();

                callback?.Invoke();                     // net 4.0
            }
        }

    }
}
