using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;

public class LocalSysCounting : MonoBehaviour {

    public static bool enableCounging;
    public string filepath = "C:/VOH/setting.xml";

    [Space, Tooltip("Use second in this script.")]
    public int limitTime = 86400;
    public int worktime = 5;
    
    public int GetWorktime {
        get { return worktime; }
    }

    [Space]
    public int seconds;

    float time;
    bool start = false;

    void Awake () {
        LoadFromXml(filepath);
    }


    public void init()
    {

        if (enableCounging)
        {

            if (worktime < limitTime)
            {
                print("Pass");
                start = true;
            }
            else
            {
                print("Deny");
                Application.Quit();
            }

        }

    }

    void FixedUpdate ()
    {
        if (enableCounging)
        {
            if (start)
            {
                time += Time.fixedDeltaTime;
                seconds = Convert.ToInt32(time);
            }
        }

    }


    private void OnApplicationQuit()
    {
        worktime += seconds;
        WriteToXml(filepath);
    }



    void LoadFromXml(string path)
    {

        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);


            if (File.Exists(path))
            {
                print("Load XML Success!!");

                xmlDoc.Load(path);

                XmlNodeList _client = xmlDoc.GetElementsByTagName("time");

                worktime = int.Parse(_client.Item(0).InnerText);

            }
            else {
                print("quit");
                Application.Quit();
            }

        }


        catch (Exception e) {
            print("quit");
            Application.Quit();
        }

    }


    void WriteToXml(string path)
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(path))
        {
            print("Write XML Success!!");
            xmlDoc.Load(path);

            XmlElement elmRoot = xmlDoc.DocumentElement;
            elmRoot.RemoveAll();                                    
            XmlElement elmNew = xmlDoc.CreateElement("name");   
            XmlElement _value = xmlDoc.CreateElement("time");      

            _value.InnerText = worktime.ToString();                 
            elmNew.AppendChild(_value);
            elmRoot.AppendChild(elmNew);

            xmlDoc.Save(path);                   
        }
    }


}


/// XML Sample
/// 
/// <? xml version="1.0" encoding="utf-8"?>
/// <rumu>
///   <name>
///     <time>433</time>
///   </name>
/// </rumu>
/// 