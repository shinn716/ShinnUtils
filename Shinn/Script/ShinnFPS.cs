using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinnFPS : MonoBehaviour {

	public float updateInterval = 0.5f;
	private float accum = 0.0f;
	private int frames = 0;
	private float timeleft;
	private string fps;
	public bool showFPS = false;
	public bool showTime = false;
	public GUIStyle myStyle;

	void Start () {
		timeleft = updateInterval;
	}

	void Update () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;

		if( timeleft <= 0.0 ) {
			fps = "" + (accum/frames).ToString("f2");
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}
	}

	void OnGUI () {

		if(showFPS)
			GUI.Label(new Rect (Screen.width-100, 0, 70, 20), "FPS " + fps, myStyle);

		if(showTime)
			GUI.Label(new Rect (Screen.width-100, 20, 70, 40), "Time " + Time.time.ToString("F2"), myStyle);
	}

}
