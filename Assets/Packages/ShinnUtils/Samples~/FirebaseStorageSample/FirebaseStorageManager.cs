// Sample:
// FirebaseStorageManager.UploadCo(fullpath, "Config", (v) => { print("Upload success! " + v); });
//

#if Enable_FirebaseStorage

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using Firebase.Storage;

public static class FirebaseStorageManager
{
    private static FirebaseStorage storage;
    private static StorageReference storage_ref;
    private static readonly string gsUrl = "gs://appurl";

    public static IEnumerator UploadCo(string _fullname, string _tarFolder, Action<string> _callback)
    {
        storage = FirebaseStorage.DefaultInstance;
        storage_ref = storage.GetReferenceFromUrl(gsUrl);

        string filename = Path.GetFileName(_fullname);
        StorageReference image_ref = storage_ref.Child(_tarFolder + "/" + filename);

        var uploadTask = image_ref.PutFileAsync(_fullname);
        yield return new WaitUntil(() => uploadTask.IsCompleted);

        if (uploadTask.Exception != null)
        {
            Debug.LogError($"Failed to upload because {uploadTask.Exception}");
            yield break;
        }

        var getUrltask = image_ref.GetDownloadUrlAsync();
        yield return new WaitUntil(() => getUrltask.IsCompleted);

        if (getUrltask.Exception != null)
        {
            Debug.LogError($"Failed to get a download url with {getUrltask.Exception}");
            yield break;
        }
        _callback?.Invoke(getUrltask.Result.ToString());
    }
    public static IEnumerator DownloadImgCo(string url, Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
        {
            var text2d = ((DownloadHandlerTexture)request.downloadHandler).texture;
            callback?.Invoke(text2d);
        }
    }
    public static IEnumerator DownloadTextCo(string url, Action<string> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            callback?.Invoke(request.downloadHandler.text);
    }
}

#endif