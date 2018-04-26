using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

	public class RotateSelf : MonoBehaviour {

		[Header("Random Speed")]
		[SerializeField] float speed;
			
		[Header("Noise Rotation, Noise Xscale")]
		[SerializeField] bool NoiseSt;
		[SerializeField] float Scale = 1;
		[Range(0, 1)] float NoiseRange = 0;

		void FixedUpdate () {

			if (NoiseSt)
				transform.Rotate (Vector3.up * Mathf.PerlinNoise (Time.time * speed, NoiseRange) * Scale, Space.Self);
			else
				transform.Rotate (Vector3.up * Time.deltaTime * speed, Space.Self);
			
		}

	}
}