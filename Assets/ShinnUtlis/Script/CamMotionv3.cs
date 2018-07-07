using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class CamMotionv3 : MonoBehaviour {

	[Space]
	public ViewPoint[] viewpoint;

	[Button]
	public void Go(int index){

		iTween.MoveTo (gameObject, iTween.Hash("position", viewpoint[index].target, "easetype", viewpoint[index].ease, "time", viewpoint[index].time, "delay", viewpoint[index].delaytime));
		iTween.RotateTo (gameObject, iTween.Hash("rotation", viewpoint[index].target, "islocal", true, "time", viewpoint[index].rottime));
	}


}

[System.Serializable]
public class ViewPoint{

	public Transform target;
	public iTween.EaseType ease = iTween.EaseType.easeInOutExpo;

	public float delaytime=0;
	public float time=5;
	public float rottime=5;

}




