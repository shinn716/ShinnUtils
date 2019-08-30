using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinn.CameraTools;

public class test : MonoBehaviour
{
    MouseController mouseController;

    void Start()
    {
        mouseController = new MouseController();
        mouseController.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        mouseController.Loop();
    }

    private void OnDestroy()
    {
        mouseController.Dispose();
    }
}
