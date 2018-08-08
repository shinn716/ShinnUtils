using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSetting : MonoBehaviour {

    public Vector2 ScreenResolution = new Vector2(1920, 281);
    public bool FullScreen = true;
    public bool FiToScreen = false;

	void Awake () {

        if (FiToScreen)
        {
            Screen.SetResolution((int)ScreenResolution.x, (int)ScreenResolution.y, FullScreen);
        }
		
	}

}
