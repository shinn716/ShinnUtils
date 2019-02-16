using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;                                  //Api compatibikityLevel .NET 2.0
using System.Threading;

public class SimpleSerialWrite : MonoBehaviour {
    
    public string PORT_NAME = "COM10";
    public int Baudrate = 9600;
    private SerialPort sp;

    private void Start () {

        // SerialPort.
        try
        {
            sp = new SerialPort("\\\\.\\" + PORT_NAME, Baudrate);
            sp.Open();
            sp.ReadTimeout = 50;
        }
        catch (System.IO.IOException e) { }
        catch (System.InvalidOperationException e) { }
    }

    private void Update () {

        if (!sp.IsOpen) { return; }


        //Example
        //if (Input.GetKeyDown(KeyCode.Q)) {
        //    print("q");
        //    Write((int)'q');
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    print("w");
        //    Write((int)'w');
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    print("e");
        //    Write((int)'e');
        //}
    }

    public void Write(byte data)
    {
        byte[] senddata = new byte[] {data, 2 };
        sp.Write(senddata, 0, senddata.Length);
    }

    private void OnApplicationQuit()
    {
        if (sp != null && sp.IsOpen)
            sp.Close();
    }
}
