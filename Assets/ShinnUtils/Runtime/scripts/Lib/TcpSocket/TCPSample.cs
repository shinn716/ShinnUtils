using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCPSample : MonoBehaviour
{
    private Shinn.Common.TCPClient m_tCPClient;
    private Shinn.Common.TCPServer m_tCPServer;

    IEnumerator Start()
    {
        yield return new WaitUntil(InitTCPServer);
        yield return new WaitUntil(InitTCPClient);
    }

    bool InitTCPServer()
    {
        m_tCPServer = new Shinn.Common.TCPServer("127.0.0.1", 6969, GetServerReceive);
        //m_tCPServer.eventReceiveCallback += GetServerReceive;
        //m_tCPServer.eventSendCallback += GetServerEcho;
        return true;
    }

    bool InitTCPClient()
    {
        m_tCPClient = new Shinn.Common.TCPClient("127.0.0.1", 6969, GetClientReceive);
        //m_tCPClient.eventReceiveCallback += GetClientReceive;
        //m_tCPClient.eventSendCallback += GetClientEcho;
        return true;
    }

    private void OnApplicationQuit()
    {
        //m_tCPServer.eventReceiveCallback -= GetServerReceive;
        //m_tCPServer.eventSendCallback -= GetServerEcho;

        //m_tCPClient.eventReceiveCallback -= GetClientReceive;
        //m_tCPClient.eventSendCallback -= GetClientEcho;

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

        //if (m_tCPClient.GetEcho() != null)
        //{
        //    print("[Client-echo]" + m_tCPClient.GetEcho());
        //}
    }

    void GetClientReceive(string callback)
    {
        print("[Client-Receive]" + callback);

        //if (m_tCPClient.GetReceiveData() != null)
        //{
        //    print("[Client-Receive]" + m_tCPClient.GetReceiveData());
        //}
    }



    void GetServerEcho(string callback)
    {
        print("[Server-echo]" + callback);

        //if (m_tCPServer.GetEcho() != null)
        //{
        //    print("[Server-echo]" + m_tCPServer.GetEcho());
        //}
    }

    void GetServerReceive(string callback)
    {
        print("[Server-Receive]" + callback);

        //if (m_tCPServer.GetReceiveData() != null)
        //{
        //    print("[Server-Receive]" + m_tCPServer.GetReceiveData());
        //}
    }
}