using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadFileFromPath : MonoBehaviour 
{
    #region Path
    public enum ReadPath
    {
        persistentDataPath,
        streamingAssetsPath,
        dataPath,
        temporaryCachePath,
    }

    public string PathSelect(ReadPath pathstatus)
    {
        switch (pathstatus)
        {
            default:
                return Application.persistentDataPath;
            case ReadPath.persistentDataPath:
                return Application.persistentDataPath;
            case ReadPath.streamingAssetsPath:
                return Application.streamingAssetsPath;
            case ReadPath.dataPath:
                return Application.dataPath;
            case ReadPath.temporaryCachePath:
                return Application.temporaryCachePath;
        }
    }
    #endregion

    #region Extenstion
    public enum ReadExtension
    {
        OBJ,
        FBX,

        JPG,
        PNG,

        MP3,
        MP4,

        TXT
    }

    public string ExtensionSelect(ReadExtension extensionStatus)
    {
        switch (extensionStatus)
        {
            default:
                return ".obj";
            case ReadExtension.OBJ:
                return ".obj";
            case ReadExtension.FBX:
                return ".fbx";
            case ReadExtension.JPG:
                return ".jpg";
            case ReadExtension.PNG:
                return ".png";
            case ReadExtension.MP3:
                return ".mp3";
            case ReadExtension.MP4:
                return ".mp4";
            case ReadExtension.TXT:
                return ".txt";
        }
    }
    #endregion

    public ReadPath readpath = ReadPath.persistentDataPath;
    public ReadExtension readextension = ReadExtension.OBJ;

    private List<string> names = new List<string>();
    public List<string> GetNames { get { return names; } }

    private List<string> contents = new List<string>();
    public List<string> GetContents { get { return contents; } }

    private void Start()
    {
        LoadFiles();
    }

    [ContextMenu("LoadFiles")]
    public void LoadFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(PathSelect(readpath));
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            if (Path.GetExtension(f.Name) == ExtensionSelect(readextension))
            {
                Debug.Log("Read files name: " + f.FullName);
                names.Add(f.FullName);
                contents.Add(LoadTxt(f.FullName));
            }
        }
    }

    [ContextMenu("RemoveAllFiles")]
    public void RemoveAllFiles()
    {
        DirectoryInfo dir = new DirectoryInfo(PathSelect(readpath));
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            File.Delete(f.ToString());
            Debug.Log("Delet files: " + f.Name);
        }

        contents.Clear();
        names.Clear();
    }


    // Convert object data to txt, 讀取Obj用
    private void WritetoTxt(string filepath, string content)
    {
        byte[] bytes = System.Convert.FromBase64String(content);
        File.WriteAllBytes(filepath, bytes);
    }

    // 讀取 txt檔, 供讀取模型使用
    private string LoadTxt(string path)
    {
        using (StreamReader r = new StreamReader(path))
        {
            string myobj = r.ReadToEnd();
            return myobj;
        }
    }
}
