//
// TCPClient - Unity TCP Socket
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
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Shinn.Common
{
    public class TCPClient
    {
        public event Action<string> Receiver;
        public string Ip { get; set; }
        public int Port { get; set; }


        private TcpClient socketConnection;
        private Thread clientReceiveThread;
        private CancellationTokenSource cancellation;
        private readonly SynchronizationContext syncContext;


        public TCPClient(string _ip = "127.0.0.1", int _port = 6969)
        {
            Ip = _ip;
            Port = _port;
            // Capture the creating thread's context (typically Unity's main thread)
            // so received messages can be marshalled back for safe Unity API access.
            syncContext = SynchronizationContext.Current;

            ConnectToTcpServer();
            Debug.Log($"Init TCPClient {Ip}/{Port}");
        }

        /// <summary>
        /// Close TcpClient
        /// </summary>
        public void Dispose()
        {
            cancellation?.Cancel();
            socketConnection?.Close();
            socketConnection = null;

            if (clientReceiveThread != null && clientReceiveThread.IsAlive)
                clientReceiveThread.Join(500);
            clientReceiveThread = null;

            cancellation?.Dispose();
            cancellation = null;
        }

        /// <summary>
        /// Send message to server using socket connection.
        /// </summary>
        public void SendMessage(string message)
        {
            if (socketConnection == null)
                return;

            try
            {
                // Get a stream object for writing.
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    // Convert string message to byte array.
                    byte[] clientMessageAsByteArray = Encoding.UTF8.GetBytes(message);
                    // Write byte array to socketConnection stream.
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                }
            }
            catch (SocketException socketException)
            {
                Debug.LogError("Socket exception: " + socketException);
            }
        }


        private void ConnectToTcpServer()
        {
            try
            {
                cancellation = new CancellationTokenSource();
                var token = cancellation.Token;
                clientReceiveThread = new Thread(() => ListenForData(token));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch (Exception e)
            {
                Debug.LogError("On client connect exception " + e);
            }
        }

        private void ListenForData(CancellationToken token)
        {
            try
            {
                socketConnection = new TcpClient(Ip, Port);
                byte[] bytes = new byte[1024];

                // Reuse a single stream for the whole connection lifetime.
                NetworkStream stream = socketConnection.GetStream();
                while (!token.IsCancellationRequested)
                {
                    // Read incoming stream into byte array.
                    int length = stream.Read(bytes, 0, bytes.Length);

                    // Read returning 0 means the remote peer closed the connection.
                    if (length == 0)
                        break;

                    var incommingData = new byte[length];
                    Array.Copy(bytes, 0, incommingData, 0, length);
                    string serverMessage = Encoding.UTF8.GetString(incommingData);
                    DispatchReceived(serverMessage);
                }
            }
            catch (SocketException e)
            {
                if (!token.IsCancellationRequested)
                    Debug.LogError("Socket exception: " + e);
            }
            catch (Exception e)
            {
                // Disposing the socket during shutdown throws here; ignore that case.
                if (!token.IsCancellationRequested)
                    Debug.LogError("TCPClient listen exception: " + e);
            }
        }

        /// <summary>
        /// Marshal the callback back to the creating thread (typically Unity's main
        /// thread) so handlers can safely call Unity APIs.
        /// </summary>
        private void DispatchReceived(string message)
        {
            var handler = Receiver;
            if (handler == null)
                return;

            if (syncContext != null)
                syncContext.Post(_ => handler(message), null);
            else
                handler(message);
        }
    }
}
