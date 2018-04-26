using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShinnUtil{


public class ColliderEvent : MonoBehaviour {

		public GameObject[] PS;
		public string MainCilliderTag;

		void OnTriggerEnter(Collider other){

			if (other.transform.tag == MainCilliderTag) {
				for (int i = 0; i < PS.Length; i++)
					PS [i].SetActive (true);
			}

		}
	}

}
