using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarouselSFX : MonoBehaviour {
	
	[SerializeField] AudioClip[] clip;
	AudioSource AS;

    [SerializeField]
    bool autoStart = true;

    [Range(0, 1)]
    public float volume = 1;
    public float pitch = .5f;

    void Start () {
		AS = GetComponent<AudioSource> ();
        if (autoStart)
        {
            int index = Random.Range(0, clip.Length);
            AS.PlayOneShot(clip[index]);
        }
    }


	void Update () {
		
		if(!AS.isPlaying && autoStart)
        {
			int index = Random.Range (0, clip.Length);
			AS.PlayOneShot (clip [index], volume);
            AS.pitch = pitch;

        }

	}
}
