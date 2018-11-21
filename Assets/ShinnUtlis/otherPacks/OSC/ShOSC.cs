using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShOSC : MonoBehaviour
{

    public enum Type
    {
        Server,
        Client
    }

    public Type type;
    public bool enableOSC = false;
    public string address = "/Scene";

    public OSC[] osc;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        print("shOSC Start");

        if (type == Type.Client && enableOSC)
        {
            for (int i = 0; i < osc.Length; i++)
                osc[i].SetAddressHandler(address, OnReceive_int);
        }
    }



    void OnReceive_int(OscMessage message)
    {
        int int_receive = Convert.ToInt16(message.values[0].ToString());
        Debug.Log("Receive " + int_receive);
    }

    public void Send_int(OSC myosc, int index)
    {
        OscMessage message = new OscMessage();
        message.address = address;
        message.values.Add(index);
        myosc.Send(message);
    }

}
