using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjs : MonoBehaviour {


    public GameObject[] prefabs;
    public Vector2 InstianteCount = new Vector2(5, 10);

    [Header("Instantiate Range")]
    public Vector2 posxRange;
    public Vector2 posyRange;
    public Vector2 poszRange;

    [Header("Gizmos")]
    public float size = 10;
    public bool WireCube = false;

    void Start () {

        int count = Random.Range((int)InstianteCount.x, (int)InstianteCount.y);
        for (int i=0; i< count; i++)
        {
            GameObject go = (GameObject)Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
            float posx = transform.position.x + Random.Range(posxRange.x, posxRange.y);
            float posy = transform.position.y + Random.Range(posyRange.x, posyRange.y);
            float posz = transform.position.z + Random.Range(poszRange.x, poszRange.y);
            go.transform.localPosition = new Vector3(posx, posy, posz);
            go.transform.parent = transform;
        }
		
	}

    private void OnDrawGizmos()
    {

        if (WireCube)
        {
            Gizmos.color = Color.yellow;
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
            Gizmos.color = Color.yellow;
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
