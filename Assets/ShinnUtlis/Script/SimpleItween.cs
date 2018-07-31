using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleItween : MonoBehaviour
{



    public enum state
    {
        shakePosition,
        SP_fadein,
		SP_fadeout,
        scaleIn,
        moveto
    }

    public state mystate;

    public float time;
    public float delay = 0;
    public iTween.EaseType ease;
    public iTween.LoopType loop;
    public bool islocal = false;
	public bool ignoreTimeScalest = false;
	public bool AutoStart = true;

    [Header("ShakePosition")]
	public Vector3 shakePos;

    [Header("ScaleState")]
    public Vector3 scaleValue;

    [Header("MoveTo")]
    public Transform moveloc;


   

    void Start()
    {
        if (AutoStart)
            Select();
    }


    public void CallStart()
    {
        Select();
    }


    void Select()
    {
        switch (mystate)
        {
            case state.shakePosition:

			iTween.ShakePosition(gameObject, iTween.Hash("x", shakePos.x, "y", shakePos.y, "z", shakePos.z , "time", time,
                                                        "delay", delay, "easetype", ease,
														"looptype", loop, "islocal", islocal, "ignoretimescale", ignoreTimeScalest));
                break;

			case state.SP_fadein:
				iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "onupdate", "fadeto1", "time", time, "delay", delay, "looptype", loop, "ignoretimescale", ignoreTimeScalest));
                break;

			case state.SP_fadeout:
				iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "onupdate", "fadeto2", "time", time, "delay", delay, "looptype", loop, "ignoretimescale", ignoreTimeScalest));
                break;


            case state.scaleIn:
                iTween.ScaleFrom(gameObject, iTween.Hash(	"scale", scaleValue, "time", time,
                                                       		"delay", delay, "easetype", ease,
															"looptype", loop, "islocal", islocal, "ignoretimescale", ignoreTimeScalest));
                break;

            case state.moveto:
                iTween.MoveTo(gameObject, iTween.Hash(	"position", moveloc.position, "time", time,
                                                       	"delay", delay, "easetype", ease,
														"looptype", loop, "islocal", islocal, "ignoretimescale", ignoreTimeScalest));
                break;
        }
    }


    void fadeto1(float newvalue)
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer sp = GetComponent<SpriteRenderer>();
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, newvalue);
        }
    }

    void fadeto2(float newvalue)
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer sp = GetComponent<SpriteRenderer>();
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, newvalue);
        }
    }

}
