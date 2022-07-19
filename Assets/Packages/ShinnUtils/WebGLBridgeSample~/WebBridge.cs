using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

public class WebBridge : MonoBehaviour
{
    // Unity2Web
    [DllImport("__Internal")]
    private static extern void JSGameOver(string userName, int score);

    [DllImport("__Internal")]
    private static extern void JSUnitySend(string str);

    [DllImport("__Internal")]
    private static extern void JSUnitySendArgs(string str);



    public void GameOver()
    {
#if UNITY_WEBGL == true && UNITY_EDITOR == false
        JSGameOver("Player1", 100);
#else
        print("Unity Send: JSGameOver/Player1, 100");
#endif
    }

    public void UnitySend()
    {
#if UNITY_WEBGL == true && UNITY_EDITOR == false
        JSUnitySend(Time.time.ToString());
#else
        print("UnitySend: JSUnitySend/" + Time.time);
#endif
    }

    public void UnitySendArgs()
    {
#if UNITY_WEBGL == true && UNITY_EDITOR == false
        JSUnitySendArgs("args");
#else
        print("UnitySend: JSUnitySendArgs/args");
#endif
    }





    // Web2Unity
    // web: unityContext.send(‘MainManager’, FromWebStr, 'json');
    // ‘MainManager’ -> Gameobject in scene 
    public void FromWebStr(string str)
    {
        print("FromWebStr: " + str);
    }
}
