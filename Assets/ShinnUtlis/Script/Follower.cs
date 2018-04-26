using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

public class Follower : MonoBehaviour {


		[SerializeField] Transform Target;
		[SerializeField] float ChaseSpeed = .1f;
		[SerializeField] float StopDist = 2;

		void FixedUpdate () {
		
			Vector3 direction = Target.transform.position - transform.position;
			direction.y = 0;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), .1f);

			if (direction.magnitude > StopDist) {
				//GetComponent<Animator> ().SetBool ("run", true);
				transform.Translate (0, 0, ChaseSpeed);
			} else {
				//GetComponent<Animator> ().SetBool ("run", false);
			}


		}
	}

}
