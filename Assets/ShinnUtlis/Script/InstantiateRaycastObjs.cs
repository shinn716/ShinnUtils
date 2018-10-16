using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstRaycast : MonoBehaviour {

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
    public GameObject[] prefab;
    public Vector2 scaleRange;
    public Vector2 rotXRange;
    public Vector2 rotYRange;
    public Vector2 rotZRange;
    public Vector2 posOffsetRange;

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

        switch (cam)
        {
            default:
                break;

            case CameraState.perspective:
                count++;
                if (count > gentime)
                {
                    count = 0;

                    GameObject go1 = Instantiate(prefab[Random.Range(0, prefab.Length)]);
                    GetRaycastPosition = GetWorldPositionOnPlane(Input.mousePosition, raycastDist);

                    go1.transform.localPosition = new Vector3(  GetRaycastPosition.x + Random.Range(posOffsetRange.x, posOffsetRange.y),
                                                                GetRaycastPosition.y + Random.Range(posOffsetRange.x, posOffsetRange.y),
                                                                GetRaycastPosition.z);
                    float scale = Random.Range(scaleRange.x, scaleRange.y);
                    float rotx = Random.Range(rotXRange.x, rotXRange.y);
                    float roty = Random.Range(rotYRange.x, rotYRange.y);
                    float rotz = Random.Range(rotZRange.x, rotZRange.y);

                    go1.transform.localScale = Vector3.one * scale;
                    go1.transform.localEulerAngles = new Vector3(rotx, roty, rotz);
                    go1.transform.parent = transform;
                }
                break;

            case CameraState.orthographic:
                count++;
                if (count > gentime)
                {
                    count = 0;

                    GameObject go2 = Instantiate(prefab[Random.Range(0, prefab.Length)]);
                    GetRaycastPosition = new Vector3(c.ScreenToWorldPoint(Input.mousePosition).x, c.ScreenToWorldPoint(Input.mousePosition).y, raycastDist);
                    go2.transform.localPosition = new Vector3(  GetRaycastPosition.x + Random.Range(posOffsetRange.x, posOffsetRange.y),
                                                                GetRaycastPosition.y + Random.Range(posOffsetRange.x, posOffsetRange.y),
                                                                GetRaycastPosition.z);

                    float scale2 = Random.Range(scaleRange.x, scaleRange.y);
                    float rotx2 = Random.Range(rotXRange.x, rotXRange.y);
                    float roty2 = Random.Range(rotYRange.x, rotYRange.y);
                    float rotz2 = Random.Range(rotZRange.x, rotZRange.y);

                    go2.transform.localScale = Vector3.one * scale2;
                    go2.transform.localEulerAngles = new Vector3(rotx2, roty2, rotz2);
                    go2.transform.parent = transform;
                }
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
