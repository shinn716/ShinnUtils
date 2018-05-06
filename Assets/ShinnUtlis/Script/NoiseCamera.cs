using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

	public class NoiseCamera : MonoBehaviour {

		[Header("NoiseCamera")]
		[SerializeField] float NoiseSpeed = .5f;

		[SerializeField]
		bool _NoiseCamViewSt;
		public bool NoiseCamViewSt {
			get{ return _NoiseCamViewSt; }
			set{ _NoiseCamViewSt = value; }
		}

		[Space]
		[SerializeField] float BaseValue = 0;
		[SerializeField] float ScaleValue = 30;

		float zoomindata;

		[Header("Noise Time Data")]
		[SerializeField] float timedata;


		void FixedUpdate () {

			if (_NoiseCamViewSt) {
				timedata += .01f;
				zoomindata = BaseValue + Mathf.PerlinNoise (timedata * NoiseSpeed, 0.0F) * ScaleValue;
				gameObject.GetComponent<Camera> ().fieldOfView = zoomindata;
			}

		}
	}

}