using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class LoadGifOnWebGL : MonoBehaviour
{
    public UniGifImage uniGifImage;

    private void Start()
    {
        string assetPath = Application.streamingAssetsPath;
        bool isWebGl = assetPath.Contains("://") ||
                         assetPath.Contains(":///");
        try
        {
            if (isWebGl)
            {
                string path = Path.Combine(assetPath, "001.gif");
                print(path);
                uniGifImage.SetGifFromUrl(path);
                uniGifImage.Play();
            }
            else // desktop app
            {
                uniGifImage.SetGifFromUrl("001.gif");
                uniGifImage.Play();
            }
        }
        catch(Exception e)
        {
            print(e);
            // handle failure
        }

    }
}
