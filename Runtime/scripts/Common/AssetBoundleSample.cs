﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AssetBoundleSample : MonoBehaviour
{
    [SerializeField]
    private string[] loadFileNames;

    void Start()
    {
        foreach(var i in loadFileNames)
            StartCoroutine(Load_gameObject(System.IO.Path.Combine(Application.streamingAssetsPath, i)));
    }
    
    #region Private
    private IEnumerator Load_gameObject(string url)
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, 0))
        {
            yield return uwr.SendWebRequest();
            if (uwr.error != null)
            {
                throw new Exception("WWW download error: " + uwr.error);
            }
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
            GameObject go = bundle.LoadAsset(bundle.name) as GameObject;
            Instantiate(go);
        }
    }
    #endregion
}
