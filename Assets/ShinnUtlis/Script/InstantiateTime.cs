using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTime : MonoBehaviour {

    public bool autoStart = true;
    public Vector2Int instTimeRange = new Vector2Int(5, 10);

    int timeRangevalue;
    float value;

    [Header("Prefabs")]
    public GameObject[] prefabs;

    [Header("Instantiate Range")]
    public Vector2 posxRange;
    public Vector2 posyRange;
    public Vector2 poszRange;

    [Header("Gizmos")]
    public float size = 10;
    public bool WireCube = false;

    void Start () {
        timeRangevalue = UnityEngine.Random.Range(instTimeRange.x, instTimeRange.y);
    }

    void FixedUpdate() {


        if (autoStart)
        {
            value += Time.deltaTime;
            int count = Convert.ToInt16(value % 60);

            if (count> timeRangevalue) {
                timeRangevalue = UnityEngine.Random.Range(instTimeRange.x, instTimeRange.y);
                value = 0;

                GameObject go = (GameObject)Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length)]);
                float posx = transform.position.x + UnityEngine.Random.Range(posxRange.x, posxRange.y);
                float posy = transform.position.y + UnityEngine.Random.Range(posyRange.x, posyRange.y);
                float posz = transform.position.z + UnityEngine.Random.Range(poszRange.x, poszRange.y);
                go.transform.localPosition = new Vector3(posx, posy, posz);
                go.transform.parent = transform;
            }
        }

    }

    private void OnDrawGizmos()
    {

        if (WireCube)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);
            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);

            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);
            Gizmos.DrawCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);
        }

        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.x), Vector3.one * size);
            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);
            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.x), Vector3.one * size);

            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.x, transform.position.z + poszRange.y), Vector3.one * size);
            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.x, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);
            Gizmos.DrawWireCube(new Vector3(transform.position.x + posxRange.y, transform.position.y + posyRange.y, transform.position.z + poszRange.y), Vector3.one * size);

        }
    }
}
