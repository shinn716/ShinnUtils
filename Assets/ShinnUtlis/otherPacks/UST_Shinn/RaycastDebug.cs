using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDebug : MonoBehaviour {

	public bool enable = false;

	void Update () {
		

		if (enable) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "trigger") {
					Debug.DrawLine (Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
					hit.transform.gameObject.SendMessage ("CubeStart");
				}
			}

		}


	}

}
