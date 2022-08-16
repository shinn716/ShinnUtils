using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.IO;
using Firebase.Storage;

public class FirebaseStorageSample : MonoBehaviour
{
    private FirebaseStorage storage;
    private StorageReference storage_ref;

    [SerializeField] private string gsUrl = "gs://url";
    [SerializeField] RawImage rawimg;


    [ContextMenu("TestUploadAndGenQRCode")]
    public void TestUploadAndGenQRCode()
    {
        StartCoroutine(UploadCo(Application.streamingAssetsPath, "Upload", "DfkhrO1XUAEYkdw.jpg", (v)=> {
            rawimg.texture = QRCodeHelper.CreateQRCodeTex2D(v);
        }));
    }


    public IEnumerator UploadCo(string locUrl, string tarUrl, string filename, Action<string> callback)
    {
        storage = FirebaseStorage.DefaultInstance;
        storage_ref = storage.GetReferenceFromUrl(gsUrl);

        string local_file = Path.Combine(locUrl, filename);
        //string local_file = "file://" + Application.streamingAssetsPath + "/" + "DfkhrO1XUAEYkdw.jpg";\

        StorageReference image_ref = storage_ref.Child(tarUrl + "/" + filename);

        var uploadTask = image_ref.PutFileAsync(local_file);
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
        callback?.Invoke(getUrltask.Result.ToString());
    }

    public IEnumerator DownloadImgCo(string url, Action<Texture2D> callback)
    {
        Texture2D text2d;
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            text2d = ((DownloadHandlerTexture)request.downloadHandler).texture;
            callback?.Invoke(text2d);
        }
    }
}
