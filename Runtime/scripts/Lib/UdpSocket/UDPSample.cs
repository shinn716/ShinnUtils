using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPSample : MonoBehaviour
{
    private Shinn.Common.UDPServer m_UdpServer;
    private Shinn.Common.UDPClient m_UdpClient;

    IEnumerator Start()
    {
        yield return new WaitUntil(InitUDPServer);
        yield return new WaitUntil(InitUDPClient);
    }

    private void OnApplicationQuit()
    {
        //m_UdpServer.callback -= GetServerReceive;

        m_UdpServer.Dispose();
        m_UdpClient.Dispose();
    }

    bool InitUDPServer()
    {
        m_UdpServer = new Shinn.Common.UDPServer("127.0.0.1", 10000, GetServerReceive);
        //m_UdpServer.callback += GetServerReceive;
        return true;
    }

    bool InitUDPClient()
    {
        m_UdpClient = new Shinn.Common.UDPClient();
        return true;
    }


    void GetServerReceive(string callback)
    {
        print("[GetServerReceive]" + callback);

        //if (m_UdpServer.GetReceiveData() != null)
        //{
        //    print("[Server-Receive]" + m_UdpServer.GetReceiveData());
        //}
    }

    [ContextMenu("ClientSend")]
    void ClientSend()
    {
        print("[ClientSend]");
        m_UdpClient.SendDataString("[Client-Send] Hello");
    }

}
