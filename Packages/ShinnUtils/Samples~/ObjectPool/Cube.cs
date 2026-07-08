using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, Shinn.IPooledObject
{
    public void OnObjectSwam()
    {

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = new Color(Random.Range(0f, 1f),
                                            Random.Range(0f, 1f),
                                            Random.Range(0f, 1f));

        print("Cube");
    }

}
