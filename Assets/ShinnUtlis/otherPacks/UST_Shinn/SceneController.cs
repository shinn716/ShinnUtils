using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour {

	#region Debug

	[SerializeField]
	Texture2D guiBackground;

	[SerializeField]
	bool showGui = true;

	[SerializeField]
	GameObject UrgDevice;

	Urg urg;

	#endregion
	public GameObject Top;

	public Vector2 TAoffPos;
	public Vector2 scaleRate;

	public GameObject touchArea;
	public static Vector4 touchArea_value;

	//----call xml
	ReadXmlData _readXml;

	[Header("Debug mode")]
	public bool debugMode = false;
	public FramesPerSecond _fps;

	Vector3 urgScale;
	public bool showMouse;
	float px;
	float py;
	public float step=.1f;
	public GUIStyle _style;

	public float step_scale=.01f;
	float scalex;
	float scaley;


	void Start () 
	{
	
		_readXml = this.GetComponent<ReadXmlData> ();
		touchArea.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);

		_readXml.init();


		if (!debugMode) {
			
			urg = UrgDevice.GetComponent<Urg> ();
			urg.init ();


			urg.PosOffset = new Vector3 ( PlayerPrefs.GetFloat ("URG Offset X", urg.PosOffset.x), PlayerPrefs.GetFloat ("URG Offset Y", urg.PosOffset.y));
			urg._Scale = new Vector3 (PlayerPrefs.GetFloat ("Scalex", urg._Scale.x), PlayerPrefs.GetFloat ("Scaley", urg._Scale.y));


			urg.Rotate = PlayerPrefs.GetFloat ("Rotate", urg.Rotate);
			urg.DetectRange = PlayerPrefs.GetFloat ("DetectRange", urg.DetectRange);

			TAoffPos.x = PlayerPrefs.GetFloat ("Touch AreaPosX", TAoffPos.x);
			TAoffPos.y = PlayerPrefs.GetFloat ("Touch AreaPosY", TAoffPos.y);
			scaleRate.x = PlayerPrefs.GetFloat ("Touch Width", scaleRate.x);
			scaleRate.y = PlayerPrefs.GetFloat ("Touch Height", scaleRate.y);

			//move = urg.PosOffset;											//20171218Careful


			//----20171225
			urg.DrawMesh = false;

			if (urg != null) {
				urg.Connect ();
			}

			px = urg.PosOffset.x;
			py = urg.PosOffset.y;

			scalex = urgScale.x;
			scaley = urgScale.y;

		} else {
			touchArea.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0f);
			Top.SetActive (false);
		}
			
		Top.transform.localPosition = new Vector3 (px, py, Top.transform.localPosition.z); 


	}

	void Update () 
	{

		if(Input.GetKeyDown(KeyCode.F8) ){
			print ("URG disconnect");
			urg.Disconnect ();
		}
		if(Input.GetKeyDown(KeyCode.F7) ){
			print ("URG connect");
			urg.Connect ();
		}


		if(showMouse)
			Cursor.visible = true;
		else
			Cursor.visible = false;


		if(Input.GetKeyDown("`"))
			showGui = !showGui;
		

		if (!debugMode) 
			touchAreaFuct ();


		if(Input.GetKey(KeyCode.UpArrow)){
			py += step;
			Vector3 pos = new Vector3 (urg.PosOffset.x, py, urg.PosOffset.z);
			urg.PosOffset = pos;
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			py -= step;
			Vector3 pos = new Vector3 (urg.PosOffset.x, py, urg.PosOffset.z);
			urg.PosOffset = pos;
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			px -= step;
			Vector3 pos = new Vector3 (px, urg.PosOffset.y, urg.PosOffset.z);
			urg.PosOffset = pos;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			px += step;
			Vector3 pos = new Vector3 (px, urg.PosOffset.y, urg.PosOffset.z);
			urg.PosOffset = pos;
		}


		if(Input.GetKey(KeyCode.Z)){
			scalex -= step_scale;
			Vector3 pos = new Vector3 (scalex, urg._Scale.y, urg._Scale.z);
			urg._Scale = pos;
		}

		if(Input.GetKey(KeyCode.X)){
			scalex += step_scale;
			Vector3 pos = new Vector3 (scalex, urg._Scale.y, urg._Scale.z);
			urg._Scale = pos;
		}



		if(Input.GetKey(KeyCode.A)){
			scaley -= step_scale;
			Vector3 pos = new Vector3 (urg._Scale.x, scaley, urg._Scale.z);
			urg._Scale = pos;
		}

		if(Input.GetKey(KeyCode.S)){
			scaley += step_scale;
			Vector3 pos = new Vector3 (urg._Scale.x, scaley, urg._Scale.z);
			urg._Scale = pos;
		}

		if(Input.GetKeyDown("=")){
			step *= 1.25f;
			step_scale *= 1.25f;
		}
		if(Input.GetKeyDown("-")){
			step /= 1.25f;
			step_scale /= 1.25f;
		}

		
	}

	void touchAreaFuct()
	{
		touchArea.transform.position = new Vector3 (TAoffPos.x, TAoffPos.y, touchArea.transform.position.z);
		touchArea.transform.localScale = new Vector3 (scaleRate.x, scaleRate.y);

		float tax = touchArea.transform.position.x;
		float taw = (touchArea.GetComponent<SpriteRenderer> ().bounds.extents.x) ;

		float tay = touchArea.transform.position.y;
		float tah = (touchArea.GetComponent<SpriteRenderer> ().bounds.extents.y) ;

		touchArea_value = new Vector4 ( (tax-taw), (tax+taw), (tay+tah), (tay-tah) );
	}

	void OnGUI()
	{

		if (!debugMode) {
		
			if (showGui) {
				_fps.showFPS = true;
				Top.SetActive (true);
				touchArea.GetComponent<SpriteRenderer> ().color = Color.white;

				GUIStyle style = new GUIStyle ();
				GUIStyleState styleState = new GUIStyleState ();

			
				styleState.background = guiBackground;
				styleState.textColor = Color.white;
				style.normal = styleState;


				GUI.color = new Color (1, 1, 1, 0.7f);
				GUILayout.BeginArea (new Rect (200, 0, 600, Screen.height), style);

				GUILayout.Space (20);
				GUILayout.Label (" URG Setting ('`' 'f7 UST Connect' 'f8 UST Disconnect' ) ", _style);

				var urgStatus = urg.IsConnected ? "Connected" : "Not Connected";
				GUILayout.Label (" URG Status :  " + urgStatus, _style);
				GUILayout.Label (" StartStep :  " + Urg._startstep + "  EndStep :  " + Urg._endstep, _style);

				GUILayout.Space (20);

				if (GUILayout.Button ("Show URG Data"))
					urg.DrawMesh = true;

				if (GUILayout.Button ("Hide URG Data"))
					urg.DrawMesh = false;

				GUILayout.Space (20);


				Vector3 pos;
				GUILayout.Label ("URG Offset X :  " + urg.PosOffset.x, _style);
				pos.x = GUILayout.HorizontalSlider (urg.PosOffset.x, -200, 200);

				GUILayout.Label ("URG Offset Y :  " + urg.PosOffset.y, _style);
				pos.y = GUILayout.HorizontalSlider (urg.PosOffset.y, -300, 1000);
				urg.PosOffset = new Vector3(pos.x, pos.y, 0);

				px = pos.x;
				py = pos.y;




				Vector3 temp;
				GUILayout.Label (" Scale X:  " + urg._Scale.x.ToString ("0.000"), _style);
				temp.x = GUILayout.HorizontalSlider (urg._Scale.x, 0f, 0.5f);

				GUILayout.Label (" Scale Y:  " + urg._Scale.y.ToString ("0.000"), _style);
				temp.y = GUILayout.HorizontalSlider (urg._Scale.y, 0f, 0.5f);
				urg._Scale = new Vector3(temp.x, temp.y, 1);

				scalex = temp.x;
				scaley = temp.y;




				GUILayout.Label (" Rotate :  " + urg.Rotate.ToString ("0"), _style);
				urg.Rotate = GUILayout.HorizontalSlider (urg.Rotate, -360, 360);

				GUILayout.Label (" DetectRange :  " + urg.DetectRange.ToString ("0.0"), _style);
				urg.DetectRange = GUILayout.HorizontalSlider (urg.DetectRange, 10f, 40000f);

				GUILayout.Space (20);

				GUILayout.Label (" Touch AreaPosX :  " + TAoffPos.x.ToString ("0.0"), _style);
				TAoffPos.x = GUILayout.HorizontalSlider (TAoffPos.x, -200f, 200f);

				GUILayout.Label (" Touch AreaPosY :  " + TAoffPos.y.ToString ("0.0"), _style);
				TAoffPos.y = GUILayout.HorizontalSlider (TAoffPos.y, -200f, 200f);


				GUILayout.Label (" Touch Width :  " + scaleRate.x.ToString ("0.0"), _style);
				scaleRate.x = GUILayout.HorizontalSlider (scaleRate.x, 0.1f, 100);

				GUILayout.Label (" Touch Height :  " + scaleRate.y.ToString ("0.0"), _style);
				scaleRate.y = GUILayout.HorizontalSlider (scaleRate.y, 0.1f, 100);

				GUILayout.Space (20);
			

				if (GUILayout.Button ("SAVE"))
					SaveData ();

				GUILayout.EndArea ();

			} else {
				_fps.showFPS = false;
				Top.SetActive (false);
				touchArea.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
			}

		}
			
	}

	void SaveData()
	{
		print ("save");
		PlayerPrefs.SetFloat ("URG Offset X", urg.PosOffset.x);
		PlayerPrefs.SetFloat ("URG Offset Y", urg.PosOffset.y);

		PlayerPrefs.SetFloat ("Scalex", urg._Scale.x);
		PlayerPrefs.SetFloat ("Scaley", urg._Scale.y);

		PlayerPrefs.SetFloat ("Rotate",urg.Rotate);
		PlayerPrefs.SetFloat ("DetectRange", urg.DetectRange);

		PlayerPrefs.SetFloat ("Touch AreaPosX", TAoffPos.x);
		PlayerPrefs.SetFloat ("Touch AreaPosY", TAoffPos.y);
		PlayerPrefs.SetFloat ("Touch Width",  scaleRate.x);
		PlayerPrefs.SetFloat ("Touch Height", scaleRate.y);
	}

	void OnApplicationQuit()
	{
		Debug.Log ("Application end");
		PlayerPrefs.Save ();
	}

	public bool GetDebugMode(){
		return debugMode;
	}
}
