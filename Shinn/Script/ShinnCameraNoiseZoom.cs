using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinnCameraNoiseZoom : MonoBehaviour {

	[Header("NoiseCamera")]
	public bool NoiseCamViewSt;
	public float NoiseSpeed = 1;
	[Space]
	public float BaseValue = 0;
	public float ScaleValue = 30;

	float zoomindata;
	bool first = false;

	void Update () {
		
		if (NoiseCamViewSt) {
			zoomindata = BaseValue + Mathf.PerlinNoise (Time.time * NoiseSpeed, 0.0F) * ScaleValue;
			gameObject.GetComponent<Camera> ().fieldOfView = zoomindata;
		}

	}
}
