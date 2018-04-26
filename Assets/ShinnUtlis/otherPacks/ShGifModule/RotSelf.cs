using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotSelf : MonoBehaviour {

	public float min=-50;
	public float max=50;

	float d1, d2, d3;

	void Start(){
		d1 = Random.Range (min, max);
		d2 = Random.Range (min, max);
		d3 = Random.Range (min, max);
	}

	void FixedUpdate () {
		transform.Rotate (new Vector3 (d1, d2, d3) * Time.deltaTime, Space.World);
	}
}
