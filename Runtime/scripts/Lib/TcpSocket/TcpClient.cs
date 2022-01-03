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
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Shinn.Common
{
    public class TCPClient
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        
        //public event CallReceiveback eventReceiveCallback;
        //public delegate void CallReceiveback();
        //public event CallSendback eventSendCallback;
        //public delegate void CallSendback();

        //private string m_receive = string.Empty;
        //private string m_echo = string.Empty;

        private TcpClient socketConnection;
        private Thread clientReceiveThread;

        public TCPClient(string _ip = "127.0.0.1", int _port = 6969, Action<string> callback = null)
        {
            Ip = _ip;
            Port = _port;

            ConnectToTcpServer(callback);
            Debug.Log($"Init TCPClient {Ip}/{Port}");
        }

        /// <summary>
        /// Close TcpClient
        /// </summary>
        public void Dispose()
        {
            if (clientReceiveThread != null)
                clientReceiveThread.Abort();

            if (socketConnection != null)
                socketConnection.Close();
        }
        
        /// <summary> 	
        /// Send message to server using socket connection. 	
        /// </summary> 	
        public void SendMessage(string message, Action<string> callback = null)
        {
            //m_receive = string.Empty;
            //m_echo = string.Empty;

            if (socketConnection == null)
            {
                return;
            }
            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    //string clientMessage = "This is a message from one of your clients.";
                    // Convert string message to byte array.                 
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);
                    // Write byte array to socketConnection stream.                 
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    //Debug.Log("Client sent his message - should be received by server");

                    var m_echo = "[Send Success]" + message;
                    callback(m_echo);

                    //eventSendCallback?.Invoke();                     // net 4.0
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
            }
        }

        ///// <summary>
        ///// Server received message.
        ///// </summary>
        ///// <returns></returns>
        //public string GetReceiveData()
        //{
        //    return m_receive;
        //}

        ///// <summary>
        ///// echo
        ///// </summary>
        ///// <returns></returns>
        //public string GetEcho()
        //{
        //    return m_echo;
        //}

        
        // Setup socket connection. 	
        private void ConnectToTcpServer(Action<string> callback)
        {
            try
            {
                clientReceiveThread = new Thread(() => ListenForData(callback));
                //clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch (Exception e)
            {
                Debug.Log("On client connect exception " + e);
            }
        }
        
        // Runs in background clientReceiveThread; Listens for incomming data.
        private void ListenForData(Action<string> callback)
        {
            try
            {
                socketConnection = new TcpClient(Ip, Port);
                Byte[] bytes = new Byte[1024];
                while (true)
                {
                    // Get a stream object for reading 				
                    using (NetworkStream stream = socketConnection.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 					
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 						
                            string serverMessage = Encoding.ASCII.GetString(incommingData);
                            //Debug.Log("server message received as: " + serverMessage);

                            callback(serverMessage);

                            //m_receive = serverMessage;
                            //eventReceiveCallback?.Invoke();                     // net 4.0
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                Debug.Log("Socket exception: " + e);
            }
        }
    }
}