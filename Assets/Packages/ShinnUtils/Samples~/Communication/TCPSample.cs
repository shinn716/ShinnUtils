using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCPSample : MonoBehaviour
{
    private Shinn.Common.TCPClient m_tCPClient;
    private Shinn.Common.TCPServer m_tCPServer;

    void Start()
    {
        m_tCPServer = new Shinn.Common.TCPServer("127.0.0.1", 6969, GetServerReceive);
        m_tCPClient = new Shinn.Common.TCPClient("127.0.0.1", 6969, GetClientReceive);
    }

    private void OnApplicationQuit()
    {
        m_tCPServer.Dispose();
        m_tCPClient.Dispose();
    }

    [ContextMenu("ClientSend")]
    private void ClientSend()
    {
        m_tCPClient.SendMessage("C123", GetClientEcho);
    }

    [ContextMenu("ServerSend")]
    private void ServerSend()
    {
        m_tCPServer.SendMessage("S123", GetServerEcho);
    }




    void GetClientEcho(string callback)
    {
        print("[Client-echo]" + callback);
    }

    void GetClientReceive(string callback)
    {
        print("[Client-Receive]" + callback);
    }



    void GetServerEcho(string callback)
    {
        print("[Server-echo]" + callback);
    }

    void GetServerReceive(string callback)
    {
        print("[Server-Receive]" + callback);
    }
}