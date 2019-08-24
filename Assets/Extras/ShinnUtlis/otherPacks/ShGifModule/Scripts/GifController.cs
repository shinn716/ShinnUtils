using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moments;
using Shinn;

public class GifController : MonoBehaviour {

    public Record record;
    public Recorder recorder;
    public UploadAndGenerateQrcode upload;   
    public KeyCode RecordKey = KeyCode.R;
    [ReadOnly]
    public float RecorderTime = 3;

    private void Start ()
    {
        RecorderTime = recorder.RecorderTime;
    }

    private void Update ()
    {		
		if(Input.GetKeyDown(RecordKey)){
            record.startRecord();
            StartCoroutine(End(RecorderTime));
		}
	}

    private IEnumerator End(float time){
        yield return new WaitForSeconds(time);
        record.endRecord();
        StartCoroutine(delayUpload());
	}

    private IEnumerator delayUpload()
    {
        while (!record.Finish)
        {
            //print("Loading progress: " + record.Progress + "%");
            if (record.Progress >= 95f)
            {
                upload.StartUpAndQr(Record.filename);
                break;                
            }
            yield return null;
        }
    }

}
