using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OSC))]
	public class OSC_receiver : MonoBehaviour {
	
	[Header("OSC Setting")]
    public OSC osc;
    public string address = "/test";
	
    [ReadOnly]
	public string ReveiveData = "NULL";

    [Header("Reveive Data")]
    public StringEvent stringEvents;

    private void Start()
    {
        if (osc == null)
            osc = GetComponent<OSC>();

        osc.SetAddressHandler(address, OnReceive);
    }

    private void OnReceive(OscMessage message)
    {
        print("Receive message " + message);
        ReveiveData = message.address;

        for (int i = 0; i < message.values.Count; i++)
            ReveiveData += " " + message.values[i];

        stringEvents.Invoke(ReveiveData);
    }
}
