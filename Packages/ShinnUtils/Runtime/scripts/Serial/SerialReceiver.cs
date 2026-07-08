#if ENABLE_DOT_NET4_X

// Api compatibility level need set to .NET 4.x
using System.IO.Ports;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using System;

namespace Shinn
{

public class SerialReceiver
{
    private SerialPort stream;
    private Thread thread;
    private volatile bool running = false;
    private readonly SynchronizationContext syncContext;
    private Action<string> callback;

    private string readingData = string.Empty;

    public SerialReceiver(string port = "COM4", int baudrate = 9600, Action<string> callback = null)
    {
        this.callback = callback;
        // Capture the creating thread's context (typically Unity's main thread)
        // so received data can be marshalled back for safe Unity API access.
        syncContext = SynchronizationContext.Current;

        // Get a list of serial port names.
        string[] ports = SerialPort.GetPortNames();
        Debug.Log("[Log]The following serial ports were found:");

        foreach (var i in ports)
            Debug.Log(i);

        Debug.Log($"[Log]Connect to {port} / {baudrate}");

        try
        {
            stream = new SerialPort(port, baudrate);
            stream.ReadTimeout = 50;
            stream.Open();
        }
        catch (Exception e)
        {
            Debug.LogError($"[SerialReceiver] Failed to open {port}: {e.Message}");
            return;
        }

        if (callback == null)
        {
            Debug.LogWarning("Need to input callback(string).");
            return;
        }

        running = true;
        thread = new Thread(Read) { IsBackground = true };
        thread.Start();
    }

    public void Dispose()
    {
        Debug.Log("[SerialReceiver] Dispose");
        running = false;

        if (stream != null && stream.IsOpen)
            stream.Close();
        stream = null;

        if (thread != null && thread.IsAlive)
            thread.Join(500);
        thread = null;
    }

    private void Read()
    {
        while (running && stream != null && stream.IsOpen)
        {
            try
            {
                readingData = stream.ReadLine();
                DispatchReceived(readingData);
            }
            catch (TimeoutException)
            {
                // No data arrived within ReadTimeout — normal, keep polling.
            }
            catch (Exception e)
            {
                if (running)
                    Debug.LogError("[SerialReceiver] Serial read error: " + e);
                break;
            }
        }
    }

    // Marshal the callback back to the creating thread (typically Unity's main
    // thread) so handlers can safely call Unity APIs.
    private void DispatchReceived(string data)
    {
        var handler = callback;
        if (handler == null)
            return;

        if (syncContext != null)
            syncContext.Post(_ => handler(data), null);
        else
            handler(data);
    }
}
}
#endif
