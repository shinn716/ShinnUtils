using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using EasyButtons;

namespace Shinn
{
    public class UploadAndQrcode : MonoBehaviour
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
            Color32[] color32 = useEncode(QRCodeURL, encoded.width, encoded.height);                                                            //儲存產生的QR Code
            encoded.SetPixels32(color32);                                                                                                       //設定要顯示的圖片像素
            encoded.Apply();                                                                                                                    //申請顯示圖片
            qrcodeCont.sprite = Sprite.Create(encoded, new Rect(0, 0, encoded.width, encoded.height), Vector2.zero);
        }

        private Color32[] useEncode(string textForEncoding, int width, int height)
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
            Debug.Log("localFileName " + localFileName);
            Debug.Log("uploadURL " + uploadURL);

            WWW localFile = new WWW("file:///" + localFileName);
            yield return localFile;

            if (localFile.error == null)
                Debug.Log("Loaded file successfully");
            else
            {
                Debug.Log("Open file error: " + localFile.error);
                yield break; // stop the coroutine here
            }
            WWWForm postForm = new WWWForm();
            // version 1
            //postForm.AddBinaryData("theFile",localFile.bytes);
            // version 2
            postForm.AddBinaryData("theFile", localFile.bytes, localFileName, "text/plain");
            WWW upload = new WWW(uploadURL, postForm);

            yield return upload;

            if (upload.error == null)
                Debug.Log("upload done :" + upload.text);
            else
                Debug.Log("Error during upload: " + upload.error);
        }

        private void UploadFile(string localFileName, string uploadURL)
        {
            StartCoroutine(UploadFileCo(localFileName, uploadURL));
        }
    }

}
