using System.Collections.Generic;
using UnityEngine;
using Shinn.CameraTools;
using Shinn.Common;
using System.Threading;

public class test : MonoBehaviour
{
    private MouseController mouseController;
    private LoadXml loadXml_Email;
    private UDPServer server;
    private UDPClient client;
    private CsvTools csvTools;
    private Thread receiveData;

    void Start()
    {
        mouseController = new MouseController();
        mouseController.Init(this);

        loadXml_Email = new LoadXml(new List<string> { "SMTP_Client", "SMTP_Port", "USER", "USER_Pass", "To", "Subject", "Body", "AttachFile" });
        loadXml_Email.Load(ShUnityPath.ApplicationStreamingAssetsPath, "EmailSetting.xml");


        server = new UDPServer();
        receiveData = new Thread(new ThreadStart(server.ReceiveData));
        receiveData.Start();
        server.callback += Getres;

        client = new UDPClient();

        LoadFile.LoadAllFiles(ShUnityPath.ApplicationStreamingAssetsPath, ShExtension.XML);

        csvTools = new CsvTools();
    }

    // Update is called once per frame
    void Update()
    {
        mouseController.Loop();
    }

    private void Getres()
    {
        Debug.Log ("result " + server.CallbackEvent());
    }

    private void OnApplicationQuit()
    {
        mouseController.Dispose();
        loadXml_Email.Clear();

        server.callback -= Getres;
        if (receiveData != null)
        {
            receiveData.Interrupt();
            receiveData.Abort();
        }
        
        server.Dispose();
        client.Dispose();
    }

    [ContextMenu("Test_ClientSocket")]
    private void Test_ClientSocket()
    {
        client.SendData("Hello");
    }

    [ContextMenu("Test_WriteToCsv")]
    private void Test_WriteToCsv()
    {

        string[] title = { "A", "B", "C", "D" };
        csvTools.WriteToCsv(title, "test");
    }

}
