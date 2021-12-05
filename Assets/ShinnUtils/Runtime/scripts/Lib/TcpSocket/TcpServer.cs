﻿//
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
        public string Ip { get; set; }
        public int Port { get; set; }

        public event CallReceiveback eventReceiveCallback;
        public delegate void CallReceiveback();
        public event CallSendback eventSendCallback;
        public delegate void CallSendback();

        private string m_receive = string.Empty;
        private string m_echo = string.Empty;
        private TcpListener tcpListener;
        private Thread tcpListenerThread;
        private TcpClient connectedTcpClient;

        public TCPServer(string _ip ="127.0.0.1", int _port = 6969)
        {
            Ip = _ip;
            Port = _port;

            tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
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
            m_receive = string.Empty;
            m_echo = string.Empty;

            if (connectedTcpClient == null)
                return;

            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = connectedTcpClient.GetStream();
                if (stream.CanWrite)
                {
                    //string serverMessage = "Hello.";
                    // Convert string message to byte array.                 
                    byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(message);
                    // Write byte array to socketConnection stream.               
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                    //Debug.Log("Server sent his message - should be received by client");

                    m_echo = "[Send Success]" + message;
                    eventSendCallback?.Invoke();                     // net 4.0
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
            }
        }

        /// <summary>
        /// Client receive message.
        /// </summary>
        /// <returns></returns>
        public string GetReceiveData()
        {
            return m_receive;
        }

        /// <summary>
        /// echo
        /// </summary>
        /// <returns></returns>
        public string GetEcho()
        {
            return m_echo;
        }


        private void ListenForIncommingRequests()
        {
            try
            {
                // Create listener on localhost port 8052. 			
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
                                // Convert byte array to string message. 							
                                string clientMessage = Encoding.ASCII.GetString(incommingData);
                                //Debug.Log("client message received as: " + clientMessage);

                                m_receive = clientMessage;
                                eventReceiveCallback?.Invoke();                     // net 4.0
                            }
                        }
                    }
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("SocketException " + socketException.ToString());
            }
        }
    }
}