using System.Collections.Generic;
using UnityEngine;
using Shinn.CameraTools;
using Shinn.Commom;

public class test : MonoBehaviour
{
    private MouseController mouseController;
    private LoadXml loadXml_Email;

    void Start()
    {
        mouseController = new MouseController();
        mouseController.Init(this);

        loadXml_Email = new LoadXml(new List<string> { "SMTP_Client", "SMTP_Port", "USER", "USER_Pass", "To", "Subject", "Body", "AttachFile" });
        loadXml_Email.Load(ShUnityPath.ApplicationStreamingAssetsPath, "EmailSetting.xml");

        LoadFile.LoadAllFiles(ShUnityPath.ApplicationStreamingAssetsPath, ShExtension.XML);
        print(LoadFile.GetContents[0]);
    }

    // Update is called once per frame
    void Update()
    {
        mouseController.Loop();
    }

    private void OnDestroy()
    {
        mouseController.Dispose();
		loadXml_Email.Dispose();

	}
}
