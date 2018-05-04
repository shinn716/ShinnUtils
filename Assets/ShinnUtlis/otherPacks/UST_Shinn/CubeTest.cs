using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Gameobject set tag as "trigger".
/// </summary>

public class CubeTest : MonoBehaviour {

	bool WorkOnce = false;

	void CubeStart(){
		if (!WorkOnce) {
			WorkOnce = true;
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
			StartCoroutine (back (2));
		}

	}

	IEnumerator back(float delay){
		yield return new WaitForSeconds (delay);
		gameObject.GetComponent<Renderer> ().material.color = Color.blue;
		WorkOnce = false;
	}


}
