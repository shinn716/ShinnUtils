using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinn{

public class RotateAround : MonoBehaviour {

	public float speed = 1;

	[Header("Lookat")]
	public bool lookatst = false;
	public Transform target;

	void FixedUpdate () {
		transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);

		if(lookatst)
			transform.LookAt (target);
	}
}

}
