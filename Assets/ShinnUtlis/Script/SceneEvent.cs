using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShinnUtil;

namespace ShinnUtil{

[RequireComponent(typeof(PreLoadScene))]
public class SceneEvent : MonoBehaviour {

	public KeyCode PressKey;
	void Update () {
			if (Input.GetKeyDown (PressKey))
				GetComponent<PreLoadScene> ().async.allowSceneActivation = true;
		}

	}

}


