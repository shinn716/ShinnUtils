using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
//using UnityEditor;

public class SystemLock : MonoBehaviour {

    [Header("Decode")]
    public string password = 52794298;

    [Header("System")]
    public string filepath = "C:/ProgramData/rumusyslock/setting.xml";
    public int nowDate;
    public int xmlDate;

    bool savedata = false;

    [Header("Limit Date")]
    public string datesetfilepath = "C:/VOH/dateset.xml";
    public int dateset;

    void Awake () {

        string tmpmonth;
        string tmpseconds;

        if (System.DateTime.Now.Month < 10) 
            tmpmonth = "0" + System.DateTime.Now.Month.ToString();
       else
            tmpmonth = System.DateTime.Now.Month.ToString();

        if (System.DateTime.Now.Day < 10)
            tmpseconds = "0" + System.DateTime.Now.Day.ToString();
        else
            tmpseconds = System.DateTime.Now.Day.ToString();

        string now = System.DateTime.Now.Year.ToString() + tmpmonth + tmpseconds;
        nowDate = Convert.ToInt32(now);
        
        LoadDateFromXml(datesetfilepath);
        LoadFromXml(filepath);
    }

    private void Start()
    {
     if (nowDate < xmlDate)
        {
            print("deny");
            savedata = false;
            Application.Quit();
        }
        else
        {
            if (nowDate > dateset)
            {
                print("deny");
                savedata = false;
                Application.Quit();
            }
            else
            {
                savedata = true;
                print("pass");
            }
        }
    }

    private void OnApplicationQuit()
    {
        if(savedata)
            WriteToXml(filepath);
    }


    void LoadDateFromXml(string path)
    {

        try
        {
            StreamReader reader = new StreamReader(path);
            //dateset = Convert.ToInt32(reader.ReadToEnd());
            var encoder = new Clib.Security();
            encoder.Key = password;
            dateset = Convert.ToInt32(encoder.DecryptBase64(reader.ReadToEnd()));

            reader.Close();
        }
        catch (Exception e)
        {
            print("quit");
            Application.Quit();
        }
    }



    void LoadFromXml(string path)
    {
        try
        {
            StreamReader reader = new StreamReader(path);
            //xmlDate = Convert.ToInt32(reader.ReadToEnd());
            var encoder = new Clib.Security();
            encoder.Key = password;
            xmlDate = Convert.ToInt32(encoder.DecryptBase64(reader.ReadToEnd()));

            reader.Close();
        }
        catch (Exception e)
        {
            print("quit");
            Application.Quit();
        }
    }

    void WriteToXml(string path)
    {
        var encoder = new Clib.Security();
        encoder.Key = password;
        string result = encoder.EncryptBase64(nowDate.ToString());

        File.WriteAllText(path, result);
        //AssetDatabase.ImportAsset(path);
    }

}
