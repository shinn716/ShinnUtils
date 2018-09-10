using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShinnUtil;

public class GifController : MonoBehaviour {

	public Shinn.UploadAndQrcode upload;

	void Start () {
		
	}

	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space)){
			GetComponent<Record>().startRecord();
			Invoke ("end", 3);
		}
	}


	void end(){
		GetComponent<Record>().endRecord();
		Invoke ("delayUpload", 3);
	}

	void delayUpload(){
		print ("test " + Record.filename);
		upload.StartUpAndQr (Record.filename);
	}
}
