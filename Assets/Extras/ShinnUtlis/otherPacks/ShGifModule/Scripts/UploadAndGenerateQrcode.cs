using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using UnityEngine.Networking;
using System.IO;

namespace Shinn
{
    public class UploadAndGenerateQrcode : MonoBehaviour
    {
        //----Generate QRcode
        public Image qrcodeCont;
        protected Texture2D encoded;

        //----Upload
        private string m_LocalFileName = "";
        private string m_URL;

        public string serverIP = "";
        public string UploadPHPLoc = "/2018UltraGraffiti/api/upload.php";
        public string ServerFilesLoc = "/2018UltraGraffiti/api/upload/";
        public string LocalDataLoc = "C:/UltraGraffitiData/scene2_gif/";

        private void Awake()
        {
            m_URL = serverIP + UploadPHPLoc;
            encoded = new Texture2D(256, 256);
        }
        
        public void StartUpAndQr(string fileName)
        {
            m_LocalFileName = LocalDataLoc + fileName;
            UploadFile(m_LocalFileName, m_URL);
            Debug.Log("Upload to " + m_URL + " File name " + m_LocalFileName);

            //----Generate Qrcode
            string QRCodeURL;
            QRCodeURL = serverIP + ServerFilesLoc +  fileName;
            Color32[] color32 = UseEncode(QRCodeURL, encoded.width, encoded.height);                                                            //儲存產生的QR Code
            encoded.SetPixels32(color32);                                                                                                       //設定要顯示的圖片像素
            encoded.Apply();                                                                                                                    //申請顯示圖片
            qrcodeCont.sprite = Sprite.Create(encoded, new Rect(0, 0, encoded.width, encoded.height), Vector2.zero);
        }

        private Color32[] UseEncode(string textForEncoding, int width, int height)
        {
            //開始進行編碼動作
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            return writer.Write(textForEncoding);
        }

        private IEnumerator UploadFileCo(string localFileName, string uploadURL)
        {
            //Debug.Log("localFileName " + localFileName);
            //Debug.Log("uploadURL " + uploadURL);

            byte[] data = File.ReadAllBytes(localFileName);                         // "file://"
            if (data==null)
                Debug.Log("Open file error: " + localFileName);

            WWWForm postForm = new WWWForm();
            // version 1
            //postForm.AddBinaryData("theFile",localFile.bytes);
            // version 2
            postForm.AddBinaryData("theFile", data, localFileName, "text/plain");
            UnityWebRequest www = UnityWebRequest.Post(uploadURL, postForm);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
                Debug.Log("From upload complete!");
        }

        private void UploadFile(string localFileName, string uploadURL)
        {
            StartCoroutine(UploadFileCo(localFileName, uploadURL));
        }
    }

}
