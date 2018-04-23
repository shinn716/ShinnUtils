using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShCamNoiseZoom : MonoBehaviour {

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
	float timedate;


	void FixedUpdate () {

		if (_NoiseCamViewSt) {
			timedate += .01f;
			zoomindata = BaseValue + Mathf.PerlinNoise (timedate * NoiseSpeed, 0.0F) * ScaleValue;
			gameObject.GetComponent<Camera> ().fieldOfView = zoomindata;
		}

	}
}
