using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class WebcamCapture : MonoBehaviour {
    
    public enum Resolution
    {
        FHD_1920x1080,
        HD_1280x720,
        nHD_640x360,
        XGA_1024x768,
        SVGA_800x600,
        VGA_640x480,
        DVCPRO_HD_960x720
    }
    public Resolution resolution;

    private Vector2Int resulutionSelect()
    {
        switch (resolution)
        {
            default:
                return new Vector2Int(640, 480);

            case Resolution.FHD_1920x1080:
                return new Vector2Int(1920, 1080);
            case Resolution.HD_1280x720:
                return new Vector2Int(1280, 720);
            case Resolution.nHD_640x360:
                return new Vector2Int(640, 360);
            case Resolution.XGA_1024x768:
                return new Vector2Int(1024, 768);
            case Resolution.SVGA_800x600:
                return new Vector2Int(800, 600);
            case Resolution.VGA_640x480:
                return new Vector2Int(640, 480);
            case Resolution.DVCPRO_HD_960x720:
                return new Vector2Int(960, 720);
        }
    }

    [ReadOnly]
    public string[] cameraList;
    public int CameraIndex = 0;

    [Space]
    public RawImage outputRawImage;

    private WebCamTexture c;

    private void Start()
    {
        WebCamDevice[] wcd = WebCamTexture.devices;
        if (wcd == null)
            return;

        cameraList = new String[wcd.Length];

        if (wcd.Length == 0)  
            Debug.LogWarning("找不到實體攝影機");
        else
        {
            for (int i = 0; i < wcd.Length; i++)
                cameraList[i] = wcd[i].name;

            c = new WebCamTexture(wcd[CameraIndex].name);
            outputRawImage.texture = c;

            Vector2Int resol = resulutionSelect();
            RectTransform rect = outputRawImage.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(resol.x, resol.y);
        }

        if (Application.isPlaying)
            c.Play();        
    }

    private void OnDisable()
    {
        if (c != null)        
            c.Stop();        
    }

    private void OnApplicationQuit()
    {
        if (c != null)
            c.Stop();
    }
}
