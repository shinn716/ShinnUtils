using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstantiateRaycastObjs : MonoBehaviour {

    public enum CameraState
    {
        perspective,
        orthographic
    }

    [Header("Camera State")]
    public bool enable = true;
    public Camera c;
    public CameraState camstate;
    public float raycastDist = 10;

    [Header("Instantiate State")]
    public int gentime = 100;
    public GameObject prefab;

    int count = 0;

    public Vector3 GetRaycastPosition { get; set; }

    private void Start () {
        if (c == null)
            c = Camera.main;
	}

    private void FixedUpdate () {
        if (enable) 
            Select(camstate);
	}


    private void Select(CameraState cam) {

        switch (cam) {
            default:
                break;

            case CameraState.perspective:
                count++;
                if (count > gentime)
                {
                    count = 0;
                    GameObject go1 = Instantiate(prefab);
                    GetRaycastPosition = GetWorldPositionOnPlane(Input.mousePosition, raycastDist);
                    go1.transform.localPosition = GetRaycastPosition;
                }
                break;

            case CameraState.orthographic:
                GameObject go2 = Instantiate(prefab);
                GetRaycastPosition = new Vector3(c.ScreenToWorldPoint(Input.mousePosition).x, c.ScreenToWorldPoint(Input.mousePosition).y, raycastDist);
                go2.transform.localPosition = GetRaycastPosition;
                break;
        }
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }


}
