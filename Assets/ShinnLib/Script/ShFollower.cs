using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ShFollower : MonoBehaviour {


	[SerializeField] Transform Target;
	[SerializeField] float ChaseSpeed=.1f;

	void FixedUpdate () {
		
		Vector3 direction = Target.transform.position - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), .1f);

		float dist = 2f;
		if (direction.magnitude > dist) {
			//GetComponent<Animator> ().SetBool ("run", true);
			transform.Translate (0, 0, ChaseSpeed);
		} else {
			//GetComponent<Animator> ().SetBool ("run", false);
		}

	}
}
