using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LoadScene))]
public class ShinnSceneEvent : MonoBehaviour {

	public LoadScene _load;
	public string PressKey;

	void Update () {
		if(Input.GetKeyDown(PressKey))
			_load.async.allowSceneActivation = true;
	}

}
