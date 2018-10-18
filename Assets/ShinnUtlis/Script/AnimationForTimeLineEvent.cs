using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationForTimeLineEvent : MonoBehaviour {

    public string[] actionName;
    Animator anim;

    private void Start()
    {
        if(anim == null)
            anim = GetComponent<Animator>();
    }

    public void Action(int name)
    {
        AnimatorControllerParameter param;
        for (int i = 0; i < anim.parameters.Length; i++)
        {
            param = anim.parameters[i];
            anim.SetBool(param.name, false);
        }

        print("Action " + actionName[name]);
        anim.SetBool(actionName[name], true);
    }


}
