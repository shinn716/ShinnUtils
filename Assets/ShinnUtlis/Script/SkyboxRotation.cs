using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public bool autoStart = false;
    public float speed = 1;

    void Update()
    {
        if (autoStart)
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
    }
}
