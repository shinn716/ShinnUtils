using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DelayAndPlay : MonoBehaviour {

    public AudioClip clip;
    public float delaytime = 1.5f;
    public float volume = 1;

    AudioSource AS;

	void Start () {
        AS = GetComponent<AudioSource>();
        StartCoroutine(Play(delaytime));
	}

    IEnumerator Play(float time)
    {
        yield return new WaitForSeconds(time);
        AS.PlayOneShot(clip, volume);
    }

}
