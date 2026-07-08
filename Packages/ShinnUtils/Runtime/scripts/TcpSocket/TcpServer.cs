//
// TCPServer - Unity TCP Socket
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
        private CancellationTokenSource cancellation;
        private readonly SynchronizationContext syncContext;
        private readonly object clientLock = new object();


        public TCPServer(string _ip = "127.0.0.1", int _port = 6969)
        {
            Ip = _ip;
            Port = _port;
            // Capture the creating thread's context (typically Unity's main thread)
            // so received messages can be marshalled back for safe Unity API access.
            syncContext = SynchronizationContext.Current;

            cancellation = new CancellationTokenSource();
            var token = cancellation.Token;
            tcpListenerThread = new Thread(() => ListenForIncommingRequests(token));
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
            Debug.Log($"Init TCPServer {Ip}/{Port}");
        }

        /// <summary>
        /// Stop the server and release resources.
        /// </summary>
        public void Dispose()
        {
            cancellation?.Cancel();

            // Stop() unblocks a pending AcceptTcpClient with a SocketException.
            if (tcpListener != null)
                tcpListener.Stop();

            lock (clientLock)
            {
                connectedTcpClient?.Close();
                connectedTcpClient = null;
            }

            if (tcpListenerThread != null && tcpListenerThread.IsAlive)
                tcpListenerThread.Join(500);
            tcpListenerThread = null;

            cancellation?.Dispose();
            cancellation = null;
        }

        /// <summary>
        /// Send message to the connected client.
        /// </summary>
        public void SendMessage(string message)
        {
            TcpClient client;
            lock (clientLock)
                client = connectedTcpClient;

            if (client == null || !client.Connected)
                return;

            try
            {
                // Get a stream object for writing.
                NetworkStream stream = client.GetStream();
                if (stream.CanWrite)
                {
                    byte[] serverMessageAsByteArray = Encoding.UTF8.GetBytes(message);
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
                }
            }
            catch (Exception e) when (e is SocketException || e is ObjectDisposedException || e is InvalidOperationException)
            {
                Debug.LogError("Socket exception: " + e);
            }
        }

        private void ListenForIncommingRequests(CancellationToken token)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse(Ip), Port);
                tcpListener.Start();
                byte[] bytes = new byte[1024];

                while (!token.IsCancellationRequested)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    lock (clientLock)
                        connectedTcpClient = client;

                    try
                    {
                        // Get a stream object for reading.
                        using (NetworkStream stream = client.GetStream())
                        {
                            int length;
                            // Read incoming stream into byte array.
                            while (!token.IsCancellationRequested &&
                                   (length = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                var incommingData = new byte[length];
                                Array.Copy(bytes, 0, incommingData, 0, length);
                                string clientMessage = Encoding.UTF8.GetString(incommingData);
                                DispatchReceived(clientMessage);
                            }
                        }
                    }
                    finally
                    {
                        // Clear the shared field only if it still points at this client.
                        lock (clientLock)
                        {
                            if (connectedTcpClient == client)
                                connectedTcpClient = null;
                        }
                        client.Close();
                    }
                }
            }
            catch (SocketException e)
            {
                // Thrown by Stop() during Dispose(); only surface real errors.
                if (!token.IsCancellationRequested)
                    Debug.LogError("SocketException " + e);
            }
            catch (Exception e)
            {
                if (!token.IsCancellationRequested)
                    Debug.LogError("TCPServer listen exception: " + e);
            }
        }

        // Marshal the callback back to the creating thread (typically Unity's main
        // thread) so handlers can safely call Unity APIs.
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
