using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemColorPool : MonoBehaviour {

    [System.Serializable]
    public struct state
    {
        public Gradient[] color;
    }

    public state mystate;

    [Header("Particle System")]
    public ParticleSystem[] PS;

    [Header("Trail")]
    public TrailRenderer[] trail;

    [Header("All random in array.")]
    public bool allRandomst = false;

    [Header("Change color in seconds.")]
    public bool changeColorSt = false;
    public float time;

    private void Start()
    {
        print("start");
        PickUpColor();

        if (changeColorSt)
            InvokeRepeating("PickUpColor", time, time);
    }

    void PickUpColor()
    {
        int index = Random.Range(0, mystate.color.Length);

        if (allRandomst)
            index = Random.Range(0, mystate.color.Length);

        for (int i = 0; i < PS.Length; i++)
        {
            var main = PS[i].main;
            main.startColor = new ParticleSystem.MinMaxGradient(mystate.color[index]);
        }

        for(int i=0; i< trail.Length; i++)
            trail[i].colorGradient = mystate.color[index];
    }
}
