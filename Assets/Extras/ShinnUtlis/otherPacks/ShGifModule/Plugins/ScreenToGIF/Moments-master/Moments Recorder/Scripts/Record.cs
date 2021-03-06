using UnityEngine;
using Moments;

[RequireComponent(typeof(Recorder)), AddComponentMenu("")]
public class Record : MonoBehaviour
{
    public static string filename;
    [ReadOnly]
    public string FileName;

    Recorder m_Recorder;

    private float m_Progress = 0f;
    public float Progress { get { return m_Progress; } }

    private bool m_IsSaving = false;
    

    private string m_LastFile = "";
    public bool Debug = false;

    private bool finish = false;
    public bool Finish { get { return finish; } }

    void Start()
	{
		// Get our Recorder instance (there can be only one per camera).
		m_Recorder = GetComponent<Recorder>();

		// If you want to change Recorder settings at runtime, use :
		//m_Recorder.Setup(autoAspect, width, height, fps, bufferSize, repeat, quality);

		// The Recorder starts paused for performance reasons, call Record() to start
		// saving frames to memory. You can pause it at any time by calling Pause().
		// Optional callbacks (see each function for more info).
	}

	public void startRecord(){
		m_Recorder.Record();
		m_Recorder.OnPreProcessingDone = OnProcessingDone;
		m_Recorder.OnFileSaveProgress = OnFileSaveProgress;
		m_Recorder.OnFileSaved = OnFileSaved;

        finish = false;
    }

	string tmp;


    public void endRecord(){
		m_Recorder.Save();
		m_Progress = 0f;
        filename = m_Recorder.GetComponent<Recorder>().OutPutname + ".gif";
        FileName = filename;

    }

    private void OnProcessingDone()
	{
		// All frames have been extracted and sent to a worker thread for compression !
		// The Recorder is ready to record again, you can call Record() here if you don't
		// want to wait for the file to be compresse and saved.
		// Pre-processing is done in the main thread, but frame compression & file saving
		// has its own thread, so you can save multiple gif at once.

		m_IsSaving = true;
	}

    private void OnFileSaveProgress(int id, float percent)
	{
        // This callback is probably not thread safe so use it at your own risks.
        // Percent is in range [0;1] (0 being 0%, 1 being 100%).
        //print(m_Progress);
        m_Progress = percent * 100f;
		if(m_Progress>=98.5f){
			print("gif-ok");
            finish = true;
        }
	}

	void OnFileSaved(int id, string filepath)
	{
		// Our file has successfully been compressed & written to disk !
		m_LastFile = filepath;

		m_IsSaving = false;

		// Let's start recording again (note that we could do that as soon as pre-processing
		// is done and actually save multiple gifs at once, see OnProcessingDone().
		m_Recorder.Record();
	}

	void OnDestroy()
	{
		// Memory is automatically flushed when the Recorder is destroyed or (re)setup,
		// but if for some reason you want to do it manually, just call FlushMemory().
		//m_Recorder.FlushMemory();
	}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        print("save");
    //        // recordStart();
    //        // Compress & save the buffered frames to a gif file. We should check the State
    //        // of the Recorder before saving, but for the sake of this example we won't, so
    //        // you'll see a warning in the console if you try saving while the Recorder is
    //        // processing another gif.
    //        m_Recorder.Save();
    //        m_Progress = 0f;
    //    }
    //}

    void OnGUI()
    {
        if (Debug)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginVertical();

            GUILayout.Space(10f);
            GUILayout.Label("Press [SPACE] to export the buffered frames to a gif file.");
            GUILayout.Label("Recorder State : " + m_Recorder.State.ToString());

            if (m_IsSaving)
                GUILayout.Label("Progress Report : " + m_Progress.ToString("F2") + "%");

            if (!string.IsNullOrEmpty(m_LastFile))
                GUILayout.Label("Last File Saved : " + m_LastFile);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}
