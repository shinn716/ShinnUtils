using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShinnPreLoadScene))]
public class ShinnSceneEvent : MonoBehaviour {

	public ShinnPreLoadScene _load;
	public string PressKey;

	void Update () {
		if(Input.GetKeyDown(PressKey))
			_load.async.allowSceneActivation = true;
	}

}
