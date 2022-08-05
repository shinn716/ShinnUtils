using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPSample : MonoBehaviour
{
    private Shinn.Common.UDPServer m_UdpServer;
    private Shinn.Common.UDPClient m_UdpClient;

    void Start()
    {
        m_UdpServer = new Shinn.Common.UDPServer("127.0.0.1", 10000, GetServerReceive);
        m_UdpClient = new Shinn.Common.UDPClient();
    }

    private void OnApplicationQuit()
    {
        m_UdpServer.Dispose();
        m_UdpClient.Dispose();
    }

    void GetServerReceive(string callback)
    {
        print("[GetServerReceive]" + callback);
    }

    [ContextMenu("ClientSend")]
    void ClientSend()
    {
        print("[ClientSend]");
        m_UdpClient.SendDataString("[Client-Send] Hello");
    }

}
