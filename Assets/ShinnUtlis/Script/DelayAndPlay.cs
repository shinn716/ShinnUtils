using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DelayAndPlay : MonoBehaviour {

    public float time = 1.5f;
	public AudioSource _as;
	void Start () {
		_as.enabled = false;
		StartCoroutine (PlayAudio(time));
	}

	IEnumerator PlayAudio(float delay){
		yield return new WaitForSeconds (delay);
		_as.enabled = true;
		_as.Play ();
	}

}
