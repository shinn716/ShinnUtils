#if ENABLE_DOT_NET4_X

// Api compatibility level need set to .NET 4.x
using System.IO.Ports;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using System.Threading.Tasks;
using System;

public class SerialReceiver
{
    private SerialPort stream;
    private Thread thread;

    //public delegate void Callback();
    //public Callback callback;

    private string readingData = string.Empty;

    public SerialReceiver(string port = "COM4", int baudrate = 9600, Action<string> callback = null)
    {
        // Get a list of serial port names.
        string[] ports = SerialPort.GetPortNames();
        Debug.Log("[Log]The following serial ports were found:");
        
        foreach (var i in ports)
            Debug.Log(i);

        Debug.Log($"[Log]Connect to {port} / {baudrate}");

        Task task01 = Task.Run(() =>
        {
            stream = new SerialPort(port, baudrate);
            stream.ReadTimeout = 50;
            stream.Open();
        });

        task01.Wait();

        if (callback == null)
        {
            Debug.Log("Need to input callback(string).");
            return;
        }

        Task task02 = Task.Run(() =>
        {
            thread = new Thread(() => Read(callback));
            thread.Start();
        });
    }

    public void Dispose()
    {
        Debug.Log("[SerialReceiver] Dispose");
        stream.Close();
        thread.Join();
    }

    private void Read(Action<string> _callback)
    {
        while (stream.IsOpen)
        {
            try
            {
                readingData = stream.ReadLine();
                Callback(_callback);
            }

            catch (Exception e)
            {
                readingData = null;
                Debug.Log("[LOG] Serial is not ready." + e);
            }
        }
    }

    private void Callback(Action<string> functionName)
    {
        functionName(readingData);
    }

    //private void Read()
    //{
    //    while (stream.IsOpen)
    //    {
    //        try
    //        {
    //            stringData = stream.ReadLine();
    //            callback?.Invoke();                     // net 4.0
    //        }

    //        catch (Exception)
    //        {
    //            stringData = null;
    //            //Debug.Log("[LOG]Serial is not ready." + e);
    //        }
    //    }
    //}
}
#endif