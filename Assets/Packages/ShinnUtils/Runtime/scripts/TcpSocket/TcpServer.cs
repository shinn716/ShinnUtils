//
// UDPServer - Unity TCP Socket
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Shinn.Common
{
    public class TCPServer
    {
        public event Action<string> Receiver;
        public string Ip { get; set; }
        public int Port { get; set; }

        private TcpListener tcpListener;
        private Thread tcpListenerThread;
        private TcpClient connectedTcpClient;


        public TCPServer(string _ip ="127.0.0.1", int _port = 6969)
        {
            Ip = _ip;
            Port = _port;

            tcpListenerThread = new Thread(() => ListenForIncommingRequests());
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
            Debug.Log($"Init TCPServer {Ip}/{Port}");
        }

        /// <summary>
        /// Close TcpClient
        /// </summary>
        public void Dispose()
        {
            if (tcpListenerThread != null)
                tcpListenerThread.Abort();

            if (tcpListener != null)
                tcpListener.Stop();
        }
        
        /// <summary> 	
        /// Send message to client using socket connection. 	
        /// </summary> 	
        public void SendMessage(string message)
        {
            if (connectedTcpClient == null)
                return;

            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = connectedTcpClient.GetStream();
                if (stream.CanWrite)
                {                
                    byte[] serverMessageAsByteArray = Encoding.UTF8.GetBytes(message);
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                    Receiver?.Invoke(message);
                }
            }
            catch (SocketException e)
            {
                Debug.Log("Socket exception: " + e);
            }
        }

        private void ListenForIncommingRequests()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse(Ip), Port);
                tcpListener.Start();
                Byte[] bytes = new Byte[1024];
                while (true)
                {
                    using (connectedTcpClient = tcpListener.AcceptTcpClient())
                    {
                        // Get a stream object for reading 					
                        using (NetworkStream stream = connectedTcpClient.GetStream())
                        {
                            int length;
                            // Read incomming stream into byte arrary. 						
                            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                var incommingData = new byte[length];
                                Array.Copy(bytes, 0, incommingData, 0, length);						
                                string clientMessage = Encoding.UTF8.GetString(incommingData);
                                Receiver?.Invoke(clientMessage);
                            }
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                Debug.Log("SocketException " + e);
            }
        }
    }
}