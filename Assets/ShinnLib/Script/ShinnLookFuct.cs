using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinnLookFuct : MonoBehaviour {

	[Header("Look at a target")]
	public Transform LookTarget;

	[Header("Lock x,z")]
	[SerializeField]
	private bool _LockRotXZ;
	public bool LockRotXZ {
		get{ return _LockRotXZ; }
		set{ _LockRotXZ = value; }
	}

	void FixedUpdate () {
		this.gameObject.transform.LookAt (LookTarget);

		if (_LockRotXZ)
			this.gameObject.transform.eulerAngles = new Vector3 (0, this.gameObject.transform.rotation.y, 0);
	}
}
