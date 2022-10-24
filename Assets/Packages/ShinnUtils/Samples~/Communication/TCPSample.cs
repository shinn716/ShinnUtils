using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinn.Common;
using System;

public class TCPSample : MonoBehaviour
{
    TCPServer tserver;
    TCPClient tclient;

    void Start()
    {
        tserver = new TCPServer();
        tserver.Receiver += TcpServerGetData;
        tclient = new TCPClient();
        tclient.Receiver += TcpClientGetData;
    }

    private void OnApplicationQuit()
    {
        tserver.Receiver -= TcpServerGetData;
        tclient.Receiver -= TcpClientGetData;
        tserver.Dispose();
        tclient.Dispose();
    }


    void TcpServerGetData(string _data)
    {
        print("==TCP Server==" + _data);
    }

    void TcpClientGetData(string _data)
    {
        print("==TCP Client==" + _data);
    }


    [ContextMenu("TcpServerSend")]
    void TcpServerSend()
    {
        tserver.SendMessage("TCP " + DateTime.Now.ToString());
    }

    [ContextMenu("TcpClientSend")]
    void TcpClientSend()
    {
        tclient.SendMessage("TCP " + DateTime.Now.ToString());
    }
}