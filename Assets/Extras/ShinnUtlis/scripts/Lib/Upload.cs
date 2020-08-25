//
//  Upload.cs
//  Upload
//
//  Created by Shinn on 2019/9/01.
//  Copyright © 2019 Shinn. All rights reserved.
//
//
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Shinn.Common
{
    public class Upload : Singleton<Upload>
    {
        private IEnumerator UploadFileCo(string uploadURL, byte[] dataByteArray, string dataName)
        {
            //Debug.Log("uploadURL " + uploadURL);
            WWWForm postForm = new WWWForm();
            postForm.AddBinaryData("theFile", dataByteArray, dataName, "text/plain");
            UnityWebRequest www = UnityWebRequest.Post(uploadURL, postForm);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
                Debug.Log("From upload complete!");
        }

        public void FileSelect(string uploadURL, byte[] dataByteArray, string dataName)
        {
            StartCoroutine(UploadFileCo(uploadURL, dataByteArray, dataName));
        }
    }
}