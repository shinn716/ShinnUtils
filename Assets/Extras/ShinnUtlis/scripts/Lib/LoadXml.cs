//
//  LoadXml.cs
//  LoadXml
//
//  Created by Shinn on 2019/8/31.
//  Copyright © 2019 Shinn. All rights reserved.
//
//
//  // Monobehavior sample
//  LoadXml loadXml_Email;
//  void Start()
//  {
//      loadXml_Email = new LoadXml(new List<string> { "SMTP_Client", "SMTP_Port", "USER", "USER_Pass", "To", "Subject", "Body", "AttachFile" });
//      loadXml_Email.Load(UnityPath.ApplicationStreamingAssetsPath, "EmailSetting.xml");
//  }

using UnityEngine;
using System.Xml;
using System.Collections.Generic;

namespace Shinn.Common
{
    public class LoadXml
    {
        public List<string> SetTagName { get; set; }
        public List<string> GetXmlData { get; set; } = new List<string>();

        /// <summary>
        /// Need set tag.
        /// </summary>
        /// <param name="_TagNames"></param>
        public LoadXml(List<string> _TagNames)
        {
            SetTagName = _TagNames;
        }

        /// <summary>
        /// Set path and filename.
        /// </summary>
        /// <param name="pathstate"></param>
        /// <param name="filename"></param>
        /// <param name="AbsolutePath"></param>
        public void Load(ShUnityPath pathstate = ShUnityPath.ApplicationStreamingAssetsPath, string filename = "EmailSetting.xml", string AbsolutePath = "C:/Users/Shinn/Desktop/")
        {
            switch (pathstate)
            {
                case ShUnityPath.ApplicationStreamingAssetsPath:
                    ReadData(System.IO.Path.Combine(Application.streamingAssetsPath, filename));
                    break;
                case ShUnityPath.ApplicationPersistentDataPath:
                    ReadData(System.IO.Path.Combine(Application.persistentDataPath, filename));
                    break;
                case ShUnityPath.ApplicationTemporaryCachePath:
                    ReadData(System.IO.Path.Combine(Application.temporaryCachePath, filename));
                    break;
                case ShUnityPath.DataPath:
                    ReadData(System.IO.Path.Combine(Application.dataPath, filename));
                    break;
                case ShUnityPath.Absolute:
                    ReadData(System.IO.Path.Combine(AbsolutePath, filename));
                    break;
            }
        }
        
        /// <summary>
        /// Dispose
        /// </summary>
        public void Clear()
        {
            SetTagName = null;
            GetXmlData = null;
        }

        /// <summary>
        /// Read data
        /// </summary>
        /// <param name="path"></param>
        private void ReadData(string path)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                if (System.IO.File.Exists(path))
                {

                    Debug.Log("Load xml success!");
                    xmlDoc.Load(path);

                    XmlNodeList[] xmlNodes = new XmlNodeList[SetTagName.Count];

                    for (int i=0; i< xmlNodes.Length; i++)
                    {
                        xmlNodes[i] = xmlDoc.GetElementsByTagName(SetTagName[i]);
                        GetXmlData.Add(xmlNodes[i].Item(0).InnerText);
                        Debug.Log(SetTagName[i] + "   " + GetXmlData[i]);
                    }
                }
            }

            catch (System.Exception e)
            {
                Debug.Log("Not find xml. " + e);
            }
        }

    }

}
