using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://answers.unity.com/questions/525952/how-i-can-converting-a-quaternion-to-a-direction-v.html?_ga=2.181103651.562719474.1569940126-381981505.1566652556

public class angle : MonoBehaviour
{
    public Transform cube1;
    public Transform cube2;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // 四元數 算夾角
        Vector3 targetForward = cube1.transform.rotation * Vector3.up;
        Vector3 targetUp = cube2.transform.rotation * Vector3.up;

        cube2.RotateAround(cube1.transform.position, Vector3.up, 20 * Time.deltaTime);

        print(Vector3.Angle(targetUp, targetForward));
    }
}
