using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarouselSFX : MonoBehaviour {
	
	[SerializeField] AudioClip[] clip;
	AudioSource AS;

	void Start () {
		AS = GetComponent<AudioSource> ();

		int index = Random.Range (0, clip.Length);
		AS.PlayOneShot (clip [index]);
	}


	void Update () {
		
		if(!AS.isPlaying){
			int index = Random.Range (0, clip.Length);
			AS.PlayOneShot (clip [index]);
		}

	}
}
