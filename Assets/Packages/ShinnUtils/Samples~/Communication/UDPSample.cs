using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinn.Common;
using System;

public class UDPSample : MonoBehaviour
{
    UDPServer server;
    UDPClient client;

    void Start()
    {
        server = new UDPServer();
        client = new UDPClient();
        server.Receiver += UdpGetData;
    }

    private void OnApplicationQuit()
    {
        server.Receiver -= UdpGetData;
        server.Dispose();
        client.Dispose();
    }


    void UdpGetData(string _data)
    {
        print("==Udp==" + _data);
    }

    [ContextMenu("UdpSend")]
    void UdpSend()
    {
        client.SendDataString(DateTime.Now.ToString());
    }
}
