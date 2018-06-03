using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPointBehavior : MonoBehaviour {

	Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
		rend.material.color = new Color (1, 1, 0, 0);
	}

	void OnApplicationExit(){
		rend.material.color = new Color (1, 1, 0, .5f);		
	}

}
