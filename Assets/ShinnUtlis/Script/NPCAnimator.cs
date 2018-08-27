using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour {

    [System.Serializable]
    public struct state{
        public string AnimationName;
        public bool active;
    }

    public state[] mystate;

    [Space]
    public Animator anim;
    public Vector2 AnimSpeedRange = new Vector2(.5f, 2);

    float speed;

    void Start () {

        speed = Random.Range(AnimSpeedRange.x, AnimSpeedRange.y);

        if(anim==null)
            anim = GetComponent<Animator>();

        for (int i = 0; i < mystate.Length; i++) {
            if(mystate[i].active)
                StartCoroutine(DelayPlay(Random.Range(0, 1), mystate[i].AnimationName, speed));
        }
    }

    IEnumerator DelayPlay(float delay, string state, float speed)
    {
        yield return new WaitForSeconds(delay);
        anim.speed = speed;
        anim.SetBool(state, true);
    }

}
