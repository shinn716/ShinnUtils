using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;						// Api compatibikityLevel .NET 2.0
using System.Threading;

public class SimpleSerialWrite : MonoBehaviour {
    
    public string PORT_NAME = "COM10";
    public int Baudrate = 9600;
    private SerialPort sp;

    void Start () {

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
	
	void Update () {

        if (!sp.IsOpen) { return; }

        if (Input.GetKeyDown(KeyCode.Q)) {
            print("q");
            byte[] senddata = new byte[] { (int)'q', 2};
            sp.Write(senddata, 0, senddata.Length);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            print("w");
            byte[] senddata = new byte[] { (int)'w', 2 };
            sp.Write(senddata, 0, senddata.Length);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            print("e");
            byte[] senddata = new byte[] { (int)'e', 2 };
            sp.Write(senddata, 0, senddata.Length);
        }
    }
    
    void OnApplicationQuit()
    {
        if (sp != null && sp.IsOpen)
            sp.Close();
    }
}
