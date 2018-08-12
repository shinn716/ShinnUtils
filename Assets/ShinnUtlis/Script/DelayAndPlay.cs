using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DelayAndPlay : MonoBehaviour {

    public AudioClip[] clip;
    public float delaytime = 1.5f;

    [Range(0, 1)]
    public float volume = 1;

    AudioSource AS;

	void Start () {
        AS = GetComponent<AudioSource>();
        StartCoroutine(Play(delaytime));
	}
    IEnumerator Play(float time)
    {
        yield return new WaitForSeconds(time);
        int randindex = Random.Range(0, clip.Length);
        AS.PlayOneShot(clip[randindex], volume);
    }

}
