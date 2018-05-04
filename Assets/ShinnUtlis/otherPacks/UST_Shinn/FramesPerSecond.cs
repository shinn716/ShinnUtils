using UnityEngine;
using System.Collections;

public class FramesPerSecond : MonoBehaviour {
	
	public float updateInterval = 0.5f;
	private float accum = 0.0f;
	private int frames = 0;
	private float timeleft;
	private string fps;
	public bool showFPS = false;
	public GUIStyle _style;

	void Start () {
		timeleft = updateInterval;
	}

	void Update () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 ) {
			// display two fractional digits (f2 format)
			fps = "" + (accum/frames).ToString("f2");
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}
	}

	public void show(){
		showFPS = true;
	}

	public void hide(){
		showFPS = false;
	}
	
	void OnGUI () {
		if(showFPS)
			GUI.Label(new Rect (Screen.width-300, 0, 70, 20), "FPS " + fps, _style);
	}
}
