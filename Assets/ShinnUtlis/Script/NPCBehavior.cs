using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{

public class NPCBehavior : MonoBehaviour {

		[Header("NPC Moving Speed")]
		public float ChaseSpeed = .1f;

		[Header("Moving Range")]
		public float MoveRandMin = -10;
		public float MoveRandMax = 10;

		[Header("Moving Rest Time")]
		public float RestTimeMin = 0;
		public float RestTimeMax = 5;

		[Header("Show Gizmos")]
		public bool showTarget = true;

		Vector3 target;
		float timevalue;
		float RestTime;

		void Start () {
			target = transform.position;
			RestTime = Random.Range (RestTimeMin, RestTimeMax);
		}
	
		void FixedUpdate ()
		{

			Vector3 direction = target - transform.position;
			direction.y = 0;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), .1f);

			if (direction.magnitude > 1) {
				transform.Translate (0, 0, ChaseSpeed);
			} else {
				
				timevalue += Time.fixedDeltaTime;
				int seconds = (int)timevalue % 60;
	
				if(seconds>RestTime){
					timevalue = 0;
					RestTime = Random.Range (RestTimeMin, RestTimeMax);
					target = new Vector3 (transform.position.x + Random.Range (MoveRandMin, MoveRandMax), 0, transform.position.z + Random.Range (MoveRandMin, MoveRandMax));
				}


			}

		}


		void OnDrawGizmos(){
			if(showTarget){
				Gizmos.color = Color.red;
				Gizmos.DrawSphere (target, .5f);
			}
		}

	}
}
