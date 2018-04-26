using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShinnUtil;

namespace ShinnUtil{

[RequireComponent(typeof(FadeAndDead))]
public class ObjLife : MonoBehaviour {

	[Header("Life Cycle, if is end call ShFadeAndDead")]
	public float LifeTime;

	void Awake () {
		StartCoroutine (LifeCount (LifeTime));
	}

	IEnumerator LifeCount(float delay){
		yield return new WaitForSeconds (delay);
			GetComponent<FadeAndDead>().FadeOut = true;
	}
	

}

}
