using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System;

[RequireComponent(typeof(LocalSysCounting))]
public class LocalSysTimeLock : MonoBehaviour
{
    public LocalSysCounting counting;

    [Space, Tooltip("Relative location: Application.../StreamingAssets/config.xml ") ]
    public bool absoluteLoc = true;
    public string filepath = "C:/TAO_iWall/config.xml";

    bool xml_key;


    private void Start()
    {
        init();
    }

    public void init()
	{
        if (absoluteLoc)
        {
            LoadFromXml(filepath);
            LocalSysCounting.enableCounging = xml_key;
            
            counting.init();
        }
        else
        {
            string assets = (Application.streamingAssetsPath + "/config.xml").ToString();
			
            LoadFromXml(assets);
            LocalSysCounting.enableCounging = xml_key;
           
            counting.init();
        }

	}

    private void LoadFromXml(string path)
	{
		
		XmlDocument xmlDoc = new XmlDocument(); 
		
		if (File.Exists (path)) 
		{
				xmlDoc.Load (path); 
				XmlNodeList _client = xmlDoc.GetElementsByTagName ("Key");
				xml_key = bool.Parse(_client.Item(0).InnerText);
        }

	}
}



/// XML Sample
/// 
/// <?xml version="1.0" encoding="utf-8"?>
/// <Data>
///   <Key>true</Key>
/// </Data>
///
