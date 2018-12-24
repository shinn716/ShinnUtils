using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinn;

[RequireComponent(typeof(Record))]
public class GifController : MonoBehaviour {

	public UploadAndQrcode upload;   
    public KeyCode RecordKey = KeyCode.R;
    public int RecorderTime = 3;

    Record record;

    void Start () {
        record = GetComponent<Record>();
    }

	void Update ()
    {		
		if(Input.GetKeyDown(RecordKey)){
            record.startRecord();
            StartCoroutine(End(RecorderTime));
		}
	}

	IEnumerator End(float time){
        yield return new WaitForSeconds(time);
        record.endRecord();
        StartCoroutine(delayUpload());
	}

    IEnumerator delayUpload()
    {
        while (!record.finish)
        {
            //print("Loading progress: " + record.m_Progress + "%");
            if (record.m_Progress >= 97f)
            {
                print("Finish!!");
                upload.StartUpAndQr(Record.filename);
                break;                
            }
            yield return null;
        }
    }

}
