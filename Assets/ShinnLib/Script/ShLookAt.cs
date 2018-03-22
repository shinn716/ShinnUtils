using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShLookAt : MonoBehaviour {

	public Transform target;

	void FixedUpdate () {

		Vector3 direction = target.position - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), .05f);

		//--Slerp
		//Vector3 temp = target.position - transform.position;
		//Quaternion targetRotation = Quaternion.LookRotation (temp);
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.EulerAngles (0, targetRotation.y, 0), Time.fixedDeltaTime * 1f);
		//transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

	}
}
