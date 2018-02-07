using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinnLookFuct : MonoBehaviour {

	[Header("Look at a target")]
	public Transform LookTarget;

	[Header("Lock x,z")]
	public bool LockRotXZ;

	void FixedUpdate () {
		this.gameObject.transform.LookAt (LookTarget);

		if (LockRotXZ)
			this.gameObject.transform.eulerAngles = new Vector3 (0, this.gameObject.transform.rotation.y, 0);
	}
}
