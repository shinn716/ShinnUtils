using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShFadeAndDead))]
public class ShLife : MonoBehaviour {

	[Header("Life Cycle, if is end call ShFadeAndDead")]
	public ShFadeAndDead _ShFadeAndDead;
	public float LifeTime;

	void Awake () {
		StartCoroutine (LifeCount (LifeTime));
	}

	IEnumerator LifeCount(float delay){
		yield return new WaitForSeconds (delay);
		_ShFadeAndDead.FadeOut = true;
	}
	

}
