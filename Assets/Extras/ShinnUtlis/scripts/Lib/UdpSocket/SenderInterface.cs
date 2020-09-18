using Shinn.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SenderInterface : MonoBehaviour
{
    public string protocolForDebug = "FC1000";  // all sensor info

    ReceiverInterface receiver;
    UDPClient client;

    void Start()
    {
        client = new UDPClient("127.0.0.1", 10001);
    }

    [ContextMenu("DebugSend")]
    public void DebugSend()
    {
        print("Send " + protocolForDebug);
        client.SendData_String(protocolForDebug);
    }

    public void SendData(string input)
    {
        if (client != null)
        {
            client.SendData_String(input);
        }
    }

    public void Dispose()
    {
        client.Dispose();
    }
}
