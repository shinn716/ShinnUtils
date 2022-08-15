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
using System;
using System.IO;

namespace Shinn
{
    public class StorageManager : MonoBehaviour
    {
        public static StorageManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public IEnumerator DownloadCo(string _sourceUrl, string _downloadLocation, Action<float> _process = null, Action _complete = null)
        {
            var uri = new Uri(_sourceUrl);
            var path = Path.Combine(_downloadLocation, Path.GetFileName(uri.LocalPath));

            if (File.Exists(path))
            {
                Debug.Log("[File exist] " + path);
                yield return null;
            }
            else
            {
                var uwr = new UnityWebRequest(_sourceUrl)
                {
                    method = UnityWebRequest.kHttpVerbGET
                };

                var dh = new DownloadHandlerFile(path)
                {
                    removeFileOnAbort = true
                };

                uwr.downloadHandler = dh;
                uwr.SendWebRequest();

                while (!uwr.isDone)
                {
                    _process?.Invoke(uwr.downloadProgress);
                    yield return null;
                }

                if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                    Debug.LogError("[Error]，" + uwr.error);
                else
                {
                    _process?.Invoke(100);
                    _complete?.Invoke();
                }
            }
        }

        public IEnumerator UploadCo(string _destinationUrl, string _fullUrl, Action<float> _process = null, Action _complete = null)
        {
            WWWForm postForm = new WWWForm();
            byte[] content = File.ReadAllBytes(_fullUrl);
            string filename = Path.GetFileName(_fullUrl);
            string[] filenameWithoutExtension = filename.Split(char.Parse("."));

            postForm.AddBinaryData("theFile", content, filenameWithoutExtension[0], "text/plain");
            UnityWebRequest uwr = UnityWebRequest.Post(_destinationUrl, postForm);
            yield return uwr.SendWebRequest();

            while (!uwr.isDone)
            {
                _process?.Invoke(uwr.uploadProgress);
                yield return null;
            }

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError(uwr.error);
            else
            {
                _process?.Invoke(100);
                _complete?.Invoke();
            }
        }
    }
}