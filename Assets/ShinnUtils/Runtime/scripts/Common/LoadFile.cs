//
//  LoadFile.cs
//  LoadFile
//
//  Created by Shinn on 2019/8/31.
//  Copyright © 2019 Shinn. All rights reserved.
//

using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Shinn.Common
{
    public static class LoadFile
    {
        /// <summary>
        /// Get all files.
        /// </summary>
        public static List<string> GetNames { get; } = new List<string>();

        /// <summary>
        /// Get all content.
        /// </summary>
        public static List<string> GetContents { get; } = new List<string>();

        /// <summary>
        /// Load files.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="readextension"></param>
        public static void LoadAllFiles(ShUnityPath path, ShExtension readextension)
        {
            GetContents.Clear();
            GetNames.Clear();

            Debug.Log(path + " " + readextension);
            DirectoryInfo dir = new DirectoryInfo(PathSelect(path));
            FileInfo[] info = dir.GetFiles("*.*");
            foreach (FileInfo f in info)
            {
                if (Path.GetExtension(f.Name) == ExtensionSelect(readextension))
                {
                    Debug.Log("Read files name: " + f.FullName);
                    GetNames.Add(f.FullName);
                    GetContents.Add(LoadTxt(f.FullName));
                }
            }
        }

        /// <summary>
        /// Remove files.
        /// </summary>
        /// <param name="path"></param>
        public static void RemoveAllFiles(ShUnityPath path)
        {
            DirectoryInfo dir = new DirectoryInfo(PathSelect(path));
            FileInfo[] info = dir.GetFiles("*.*");
            foreach (FileInfo f in info)
            {
                File.Delete(f.ToString());
                Debug.Log("Delet files: " + f.Name);
            }

            GetContents.Clear();
            GetNames.Clear();
        }

        #region Private function
        private static string ExtensionSelect(ShExtension extensionStatus)
        {
            switch (extensionStatus)
            {
                default:
                    return ".obj";
                case ShExtension.OBJ:
                    return ".obj";
                case ShExtension.FBX:
                    return ".fbx";
                case ShExtension.JPG:
                    return ".jpg";
                case ShExtension.PNG:
                    return ".png";
                case ShExtension.MP3:
                    return ".mp3";
                case ShExtension.MP4:
                    return ".mp4";
                case ShExtension.TXT:
                    return ".txt";
                case ShExtension.XML:
                    return ".xml";
            }
        }

        private static string PathSelect(ShUnityPath path)
        {
            switch (path)
            {
                default:
                    return Application.persistentDataPath;
                case ShUnityPath.ApplicationPersistentDataPath:
                    return Application.persistentDataPath;
                case ShUnityPath.ApplicationStreamingAssetsPath:
                    return Application.streamingAssetsPath;
                case ShUnityPath.DataPath:
                    return Application.dataPath;
                case ShUnityPath.ApplicationTemporaryCachePath:
                    return Application.temporaryCachePath;
            }
        }

        // 讀取 txt檔, 供讀取模型使用
        private static string LoadTxt(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string myobj = r.ReadToEnd();
                return myobj;
            }
        }
        #endregion
    }
}