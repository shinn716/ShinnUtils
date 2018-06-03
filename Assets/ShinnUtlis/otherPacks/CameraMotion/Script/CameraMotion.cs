using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

[ExecuteInEditMode]
public class CameraMotion : MonoBehaviour {

	[Header("Setting")]
	public GameObject MovePoint;
	public bool lookatst = false;
	public float lookAtSpeed = .01f;
	public Transform target;

	[Space]
	public bool startAuto = false;
	public float speed = 5;
	public iTween.EaseType ease = iTween.EaseType.easeInOutQuint;
	public float delaytime = 2;
	bool runOnce = false;

	[Header("Point Location")]
	public Vector3[] PointLoc;
	int index=0;

	void Awake(){
		Debug.Log ("Draw in Editor");
	}

	void Start(){
		GameObject[] allpoints = GameObject.FindGameObjectsWithTag("campoint");
		PointLoc  = new Vector3[allpoints.Length];

		for (int i=0; i<allpoints.Length; i++)
			PointLoc[i] = allpoints[i].transform.position;
	}

	void FixedUpdate(){

		if (lookatst) {
			Vector3 direction = target.position - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(direction), lookAtSpeed);
		}

		if (startAuto) {

			if (!runOnce) {
				runOnce = true;

				iTween.MoveTo (gameObject, iTween.Hash ("position", PointLoc [index], "easetype", ease, "speed", speed, "delay", delaytime, "oncomplete", "end", "oncompletetarget", gameObject));

				if (index >= PointLoc.Length - 1)
					index = 0;
				else
					index++;
			}
		}
	}


	[Button]
	public void GeneratePoint()
	{
		GameObject go = (GameObject) Instantiate (MovePoint);
		go.transform.position = transform.position;
	}

	[Button]
	public void ClearPoint()
	{
		GameObject[] allpoints = GameObject.FindGameObjectsWithTag("campoint");
		foreach (GameObject points in allpoints)
			DestroyImmediate (points);
	}

	[Button]
	public void Go(){
		iTween.MoveTo (gameObject, iTween.Hash("position", PointLoc[index], "easetype", ease, "speed", speed, "delay", delaytime));

		if (index >= PointLoc.Length-1)
			index = 0;
		else
			index++;
	}

	void end(){
		runOnce = false;
	}

}
