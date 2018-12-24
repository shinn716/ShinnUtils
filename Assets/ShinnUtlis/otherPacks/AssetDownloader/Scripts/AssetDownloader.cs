using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class AssetDownloader : MonoBehaviour
{
    [Serializable]
    public struct Download
    {
        public string URL;
        public Image progressUI;
        public Text statusUI;
    }

    public bool downloadOnStart = true;
    public bool downloadSequentially = true;
    public Download[] downloads;

    private byte[] bytes;
    private Coroutine videoPlayerCoroutine;
    private Coroutine downloaderCoroutine;


    //[Header("VideoPlayer")]
    //public VideoPlayer videoPlayer;
    //public Text videoPlayerStatusUI;


    /// <summary>
    /// 下載所有影片，如果正在下載會中斷
    /// </summary>
    /// <param name="downloadSequentially">同時開始下載或者是依序下載</param>
    public void DownloadAll(bool downloadSequentially)
    {
        this.downloadSequentially = downloadSequentially;
        if (downloaderCoroutine != null)
        {
            StopCoroutine(downloaderCoroutine);
        }
        downloaderCoroutine = StartCoroutine(DownloadAllCo());
    }



    /// <summary>
    /// 刪除指定名稱的影片，如果尚未下載則沒有任何反應
    /// </summary>
    /// <param name="videoName">例如：reset.mp4</param>
    public void ClearMovie(string videoName)
    {
        var path = Path.Combine(Application.persistentDataPath, videoName);
        if (File.Exists(path))
        {
            File.Delete(path);
            var UIOfVideo = downloads.First(download =>
            {
                var uri = new Uri(download.URL);
                var filename = Path.GetFileName(uri.LocalPath);
                if (filename == videoName)
                    return true;
                else
                    return false;
            }).statusUI;
            UIOfVideo.text = "檔案已刪除";
        }
    }

    private void Start()
    {
        print(Application.persistentDataPath);

        if (downloadOnStart)
        {
            StartCoroutine(DownloadAllCo());
        }
    }

    IEnumerator DownloadAllCo()
    {
        foreach (var download in downloads)
        {
            if (downloadSequentially)
                yield return DownloadCo(download);
            else
                StartCoroutine(DownloadCo(download));
        }
    }
    

    /// <summary>
    /// 使用新的 UnityWebRequest API 來下載檔案
    /// </summary>
    /// <param name="download">包含下載的URL、顯示UI等等</param>
    /// <returns></returns>
    IEnumerator DownloadCo(Download download)
    {
        var uri = new Uri(download.URL);
        var filename = Path.GetFileName(uri.LocalPath);
        var path = Path.Combine(Application.persistentDataPath, filename);

        if (File.Exists(path))
        {
            download.statusUI.text = "檔案已存在";
            download.progressUI.fillAmount = 1;
            yield return null;
        }
        else
        {
            var uwr = new UnityWebRequest(download.URL)
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
                download.progressUI.fillAmount = uwr.downloadProgress;
                download.statusUI.text = (uwr.downloadProgress * 100).ToString("00")+"%";
                yield return null;
            }

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                download.statusUI.text = "發生錯誤，" + uwr.error;
            }
            else
            {
                download.statusUI.text = "下載完成，存檔成功";
            }
        }
    }

    ///// <summary>
    ///// 播放指定名稱的影片，如果正在播放會中斷
    ///// </summary>
    ///// <param name="videoName">例如：reset.mp4</param>
    //public void PlayVideo(string videoName)
    //{
    //    if (videoPlayerCoroutine != null)
    //    {
    //        videoPlayerStatusUI.text = "中斷播放";
    //        StopCoroutine(videoPlayerCoroutine);
    //    }
    //    videoPlayerCoroutine = StartCoroutine(PlayVideoCo(videoName));
    //}

    //IEnumerator PlayVideoCo(string videoName)
    //{
    //    videoPlayer.url = Path.Combine(Application.persistentDataPath, videoName);
    //    videoPlayerStatusUI.text = "正在準備 Video Player...";
    //    yield return null;
    //    videoPlayer.Prepare();
    //    videoPlayer.prepareCompleted += v =>
    //    {
    //        videoPlayerStatusUI.text = "準備完畢，開始播放";
    //        videoPlayer.Play();
    //    };
    //}
}
