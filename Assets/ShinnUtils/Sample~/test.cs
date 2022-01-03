using System.Collections.Generic;
using UnityEngine;
using Shinn.Common;
using System.Threading;

public class test : MonoBehaviour
{
    private LoadXml loadXml_Email;
    private CsvTools csvTools;
    private SerialReceiver serial;

    void Start()
    {
        loadXml_Email = new LoadXml(new List<string> { "SMTP_Client", "SMTP_Port", "USER", "USER_Pass", "To", "Subject", "Body", "AttachFile" });
        loadXml_Email.Load(ShUnityPath.ApplicationStreamingAssetsPath, "EmailSetting.xml");

        LoadFile.LoadAllFiles(ShUnityPath.ApplicationStreamingAssetsPath, ShExtension.XML);

        csvTools = new CsvTools();

        serial = new SerialReceiver("COM5", 9600, callback);
    }
    
    private void callback(string serial)
    {
        print(serial);
    }

    private void OnApplicationQuit()
    {
        serial.Dispose();
        loadXml_Email.Clear();

    }

    [ContextMenu("Test_WriteToCsv")]
    private void Test_WriteToCsv()
    {

        string[] title = { "A", "B", "C", "D" };
        csvTools.WriteToCsv(title, "test");
    }
}
