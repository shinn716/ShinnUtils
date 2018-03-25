using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinnRotateSelf : MonoBehaviour {

	[Header("Random Speed, Noise Xscale")]
	[SerializeField] float speed;

	[Header("Noise Rotation")]
	[SerializeField] bool NoiseSt;
	[SerializeField] float NoiseRange;

	void FixedUpdate () {

		if (NoiseSt) 
			transform.Rotate (Vector3.up * Mathf.PerlinNoise (Time.time * speed, 0.0F) * NoiseRange, Space.Self);

		else
			transform.Rotate (Vector3.up * Time.deltaTime * speed, Space.Self);
	}

}
