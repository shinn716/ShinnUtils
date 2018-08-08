using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class CamMotionv3 : MonoBehaviour {
    
	[Space]
	public ViewPoint[] viewpoint;

    public int TextIndex = 0;

    [Button]
    public void StartButton()
    {
        if (viewpoint[TextIndex].isLical)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", viewpoint[TextIndex].target.localPosition, "easetype", viewpoint[TextIndex].ease, "time", viewpoint[TextIndex].time, "delay", viewpoint[TextIndex].delaytime));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", viewpoint[TextIndex].target.localEulerAngles, "delay", viewpoint[TextIndex].delaytime, "time", viewpoint[TextIndex].rottime));
        }
        else
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", viewpoint[TextIndex].target.position, "easetype", viewpoint[TextIndex].ease, "time", viewpoint[TextIndex].time, "delay", viewpoint[TextIndex].delaytime));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", viewpoint[TextIndex].target.eulerAngles, "delay", viewpoint[TextIndex].delaytime, "time", viewpoint[TextIndex].rottime));
        }
    }


	
	public void Go(int index){

        if (viewpoint[index].isLical)
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", viewpoint[index].target.localPosition, "easetype", viewpoint[index].ease, "time", viewpoint[index].time, "delay", viewpoint[index].delaytime));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", viewpoint[index].target.localEulerAngles, "delay", viewpoint[index].delaytime, "time", viewpoint[index].rottime));
        }
        else
        {
            iTween.MoveTo(gameObject, iTween.Hash("position", viewpoint[index].target.position, "easetype", viewpoint[index].ease, "time", viewpoint[index].time, "delay", viewpoint[index].delaytime));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", viewpoint[index].target.eulerAngles, "delay", viewpoint[index].delaytime, "time", viewpoint[index].rottime));
        }

	}


}

[System.Serializable]
public class ViewPoint{

	public Transform target;
	public iTween.EaseType ease = iTween.EaseType.easeInOutExpo;

    public bool isLical = false;
	public float delaytime=0;
	public float time=5;
	public float rottime=5;

}
