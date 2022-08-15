using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using System.IO;
using Firebase.Storage;

public class FirebaseStorageSample : MonoBehaviour
{
    private FirebaseStorage storage;
    private StorageReference storage_ref;

    [SerializeField] private string gsUrl = "gs://url";
    [SerializeField] Texture2D rawimg;

    // Use this for initialization
    void Start()
    {
        Upload(Application.streamingAssetsPath, "Upload", "DfkhrO1XUAEYkdw.jpg",
            () =>
            {
                Debug.Log("Upload success!");
            });
        DownloadImg("https://firebasestorage.googleapis.com/v0/b/helloword-47662.appspot.com/o/Upload%2FDfkhrO1XUAEYkdw.jpeg?alt=media&token=c9076ec7-9098-42d0-b7ef-0f84526b8c8b",
            (v) =>
            {
                rawimg = v;
                Debug.Log("Download success!");
            });
    }

    public void DownloadImg(string url, Action<Texture2D> action = null)
    {
        StartCoroutine(DownloadImgCo(url, action));
    }

    public void Upload(string locUrl, string tarUrl, string filename, Action action = null)
    {
        storage = FirebaseStorage.DefaultInstance;
        storage_ref = storage.GetReferenceFromUrl(gsUrl);

        // File located on disk
        string local_file = Path.Combine(locUrl, filename);
        //string local_file = "file://" + Application.streamingAssetsPath + "/" + "DfkhrO1XUAEYkdw.jpg";

        //Create a reference to the file you want to upload
        StorageReference image_ref = storage_ref.Child(tarUrl + "/" + filename);

        // Upload the file to the path
        image_ref.PutFileAsync(local_file)
          .ContinueWith((Task<StorageMetadata> task) =>
          {
              if (task.IsFaulted || task.IsCanceled)
              {
                  Debug.Log(task.Exception.ToString());
                  // Uh-oh, an error occurred!
              }
              else
              {
                  //Metadata contains file metadata such as size, content - type, and download URL.
                  //StorageMetadata metadata = task.Result;
                  //string download_url = metadata.DownloadUrl.ToString();
                  action?.Invoke();
                  //Debug.Log("download url = " + download_url);
              }
          });
    }

    private IEnumerator DownloadImgCo(string url, Action<Texture2D> action)
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
            action?.Invoke(text2d);
        }
    }
}
