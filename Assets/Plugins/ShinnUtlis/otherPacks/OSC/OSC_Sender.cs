using UnityEngine;
using EasyButtons;

public class OSC_Sender : MonoBehaviour {

    [System.Serializable]
    public class SendData
    {
        [SerializeField] public string address = "/Character";
        [SerializeField] public string[] data;
    }

    public OSC osc;
    public SendData senddata;    

    private void Start()
    {
        if (osc == null)
            osc = GetComponent<OSC>();
    }

    [Button]
    public void Send() {
        OscMessage message = new OscMessage();
        message.address = senddata.address;

        for (int i = 0; i < senddata.data.Length; i++)
            message.values.Add(senddata.data[i]);

        osc.Send(message);
    }
}
