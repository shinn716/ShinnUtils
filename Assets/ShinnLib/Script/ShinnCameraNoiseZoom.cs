using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinnCameraNoiseZoom : MonoBehaviour {

	[Header("NoiseCamera")]
	public float NoiseSpeed = .5f;
	[SerializeField]
	private bool _NoiseCamViewSt;
	public bool NoiseCamViewSt {
		get{ return _NoiseCamViewSt; }
		set{ _NoiseCamViewSt = value; }
	}


	[Space]
	public float BaseValue = 0;
	public float ScaleValue = 30;

	float zoomindata;
	bool first = false;

	void Update () {
		
		if (_NoiseCamViewSt) {
			zoomindata = BaseValue + Mathf.PerlinNoise (Time.time * NoiseSpeed, 0.0F) * ScaleValue;
			gameObject.GetComponent<Camera> ().fieldOfView = zoomindata;
		}

	}
}
