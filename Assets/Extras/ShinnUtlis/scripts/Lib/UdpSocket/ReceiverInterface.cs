// Data Receiver Sample - UDP Socket
// Author : Shinn
// This Script for CEX01-Cradle(WIFI/BLE) only.

using Shinn.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ReceiverInterface : MonoBehaviour
{
    #region Declare
    [Serializable]
    public class CradleStatue
    {
        // [D]
        public bool bleEnable = false;
        public string mac = string.Empty;
        public string status = "BLE";
        public int id = 0;
        public int battery = 0;
        public string VerHW = string.Empty;
        public string VerFW = string.Empty;
        public string protocol = string.Empty;
        public List<SensorStatus> sensorList = new List<SensorStatus>();

        public CradleStatue(string _mac)
        {
            mac = _mac;
        }
    }

    [Serializable]
    public class SensorStatus
    {
        public int id = -1;
        public int sn = -1;
        public bool bleEnable = true;

        [Header("[G] Gsensor")]
        public Vector3 gsensorData = Vector3.one;
        public Vector3 gsensorEulur = Vector3.one;

        [Header("[R] Raw Data")]
        public Vector3 accelerator = Vector3.one;
        public Vector3 gyro = Vector3.one;

        [Header("[Q] Quaternion")]
        public Quaternion rawq = Quaternion.identity;
        // timestamp
        public string timeStamp = string.Empty;

        public SensorStatus(int _id, bool _enable)
        {
            id = _id;
            bleEnable = _enable;
        }
    }
    
    [Header("Raw data")]
    public List<CradleStatue> cradleList = new List<CradleStatue>();
    SenderInterface sender;
    UDPServer server;
    Thread thread;
    #endregion

    #region Public 
    // Connect 
    public bool SetConnect { get; set; } = true;
    public bool GetConnectStatus { get; set; }

    // Clear list and status
    public void Clear()
    {
        cradleList.Clear();
    }

    // Set SetSensorDefault
    public void SetSensorDefault()
    {
        for (int i = 0; i < cradleList.Count; i++)
        {
            for (int j = 0; j < cradleList[i].sensorList.Count; j++)
            {
                cradleList[i].sensorList[j].id = -1;
                cradleList[i].sensorList[j].sn = -1;
                cradleList[i].sensorList[j].bleEnable = true;
                cradleList[i].sensorList[j].gsensorData = Vector3.zero;
                cradleList[i].sensorList[j].gsensorEulur = Vector3.zero;
                cradleList[i].sensorList[j].accelerator = Vector3.zero;
                cradleList[i].sensorList[j].gyro = Vector3.zero;
                cradleList[i].sensorList[j].rawq = Quaternion.identity;
            }
        }
    }

    // Dispose thread
    public void Dispose()
    {
        SetConnect = false;
        GetConnectStatus = false;
        Clear();
        if (server != null)
        {
            server.callback -= GetRawData;
            server.Dispose();
            server = null;
        }
        thread.Join();
    }
    #endregion

    #region Main

    private void Start()
    {
        sender = GetComponent<SenderInterface>();
        server = new UDPServer();
        server.callback += GetRawData;
        thread = new Thread(new ThreadStart(server.ReceiveData));
        thread.Start();
    }

    private void OnApplicationQuit()
    {
        Dispose();
    }
    #endregion

    #region Private
    private void GetRawData()
    {
        if (!GetConnectStatus)
        {
            GetConnectStatus = true;
        }

        string data = server.CallbackEvent();
        //擷取0,1 byte
        string[] splitType = data.Split(char.Parse(" "));
        var GetType = splitType[0];

#if UNITY_EDITOR
        //Debug.Log("GetType " + GetType);
        //Debug.Log("Socket CallbackEvent: " + data);
#endif

        string[] ACK = data.Split(char.Parse(","));
        var id = Convert.ToInt32(ACK[0].Substring(4).Trim());
        var mac = ACK[1].Trim();
        var protocol = ACK[2].Trim();

        if (SetConnect)
        {
            // 接收四元數資料
            if (GetType.Trim() == "[Q]")
            {
                for (int i = 0; i < cradleList.Count; i++)
                {
                    if (mac.Equals(cradleList[i].mac))
                    {
                        float.TryParse(ACK[3].Trim(), out float qw);
                        float.TryParse(ACK[4].Trim(), out float qx);
                        float.TryParse(ACK[5].Trim(), out float qy);
                        float.TryParse(ACK[6].Trim(), out float qz);
                        // timestamp
                        var timestamp = ACK[7].Trim();

                        if (cradleList[i].sensorList.Count == 0)
                            cradleList[i].sensorList.Add(new SensorStatus(id, true));
                        else
                        {
                            bool exists = cradleList[i].sensorList.Exists(x => x.id == id);
                            if (!exists)
                                cradleList[i].sensorList.Add(new SensorStatus(id, true));
                            else
                            {
                                for (int j = 0; j < cradleList[i].sensorList.Count; j++)
                                {
                                    if (id.Equals(cradleList[i].sensorList[j].id))
                                    {
                                        cradleList[i].sensorList[j].sn = Convert.ToInt32(protocol);
                                        cradleList[i].sensorList[j].rawq = new Quaternion(qx, qy, qz, qw);
                                        cradleList[i].sensorList[j].bleEnable = true;
                                        // time stamp
                                        cradleList[i].sensorList[j].timeStamp = timestamp;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // 接收 Accelerometer 和 Gyro 資料
            if (GetType.Trim() == "[R]")
            {
                for (int i = 0; i < cradleList.Count; i++)
                {
                    if (mac.Equals(cradleList[i].mac))
                    {
                        float.TryParse(ACK[3].Trim(), out float ax);
                        float.TryParse(ACK[4].Trim(), out float ay);
                        float.TryParse(ACK[5].Trim(), out float az);
                        float.TryParse(ACK[6].Trim(), out float gx);
                        float.TryParse(ACK[7].Trim(), out float gy);
                        float.TryParse(ACK[8].Trim(), out float gz);
                        // timestamp
                        var timestamp = ACK[9].Trim();

                        if (cradleList[i].sensorList.Count == 0)
                            cradleList[i].sensorList.Add(new SensorStatus(id, true));
                        else
                        {
                            bool exists = cradleList[i].sensorList.Exists(x => x.id == id);
                            if (!exists)
                                cradleList[i].sensorList.Add(new SensorStatus(id, true));
                            else
                            {
                                for (int j = 0; j < cradleList[i].sensorList.Count; j++)
                                {
                                    if (id.Equals(cradleList[i].sensorList[j].id))
                                    {
                                        cradleList[i].sensorList[j].sn = Convert.ToInt32(protocol);
                                        cradleList[i].sensorList[j].accelerator = new Vector3(ax, ay, az);
                                        cradleList[i].sensorList[j].gyro = new Vector3(gx, gy, gz);
                                        cradleList[i].sensorList[j].bleEnable = true;
                                        // time stamp
                                        cradleList[i].sensorList[j].timeStamp = timestamp;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // 接收 Gsensor 資料
            else if (GetType.Trim() == "[G]")
            {
                for (int i = 0; i < cradleList.Count; i++)
                {
                    if (mac.Equals(cradleList[i].mac))
                    {
                        float.TryParse(ACK[3].Trim(), out float gsx);
                        float.TryParse(ACK[4].Trim(), out float gsy);
                        float.TryParse(ACK[5].Trim(), out float gsz);
                        float.TryParse(ACK[6].Trim(), out float gsex);
                        float.TryParse(ACK[7].Trim(), out float gsey);
                        float.TryParse(ACK[8].Trim(), out float gsez);

                        if (cradleList[i].sensorList.Count == 0)
                            cradleList[i].sensorList.Add(new SensorStatus(id, true));
                        else
                        {
                            bool exists = cradleList[i].sensorList.Exists(x => x.id == id);
                            if (!exists)
                                cradleList[i].sensorList.Add(new SensorStatus(id, true));
                            else
                            {
                                for (int j = 0; j < cradleList[i].sensorList.Count; j++)
                                {
                                    if (id.Equals(cradleList[i].sensorList[j].id))
                                    {
                                        cradleList[i].sensorList[j].sn = Convert.ToInt32(protocol);
                                        cradleList[i].sensorList[j].gsensorData = new Vector3(gsx, gsy, gsz);
                                        cradleList[i].sensorList[j].gsensorEulur = new Vector3(gsex, gsey, gsez);
                                        cradleList[i].sensorList[j].bleEnable = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // 接收 CEX01 一般資料 (電量 ACK ...)
        if (GetType.Trim().Equals("[D]"))
        {
            byte[] response = { 0x00 };
            response = StringToBytes(protocol.Substring(0, 4).Trim());

            // BLE Disconnect [1090]
            if (response[0] == 0x10 && response[1] == 0x90)
            {
                string senId = protocol.Substring(4, 2);
                string senEvent = protocol.Substring(6, 2);
                //print(senId + " " + Convert.ToInt32(senId, 16) + " " + senEvent);

                for (int i = 0; i < cradleList.Count; i++)
                {
                    if (mac.Equals(cradleList[i].mac))
                    {
                        for (int j = 0; j < cradleList[i].sensorList.Count; j++)
                        {
                            if (Convert.ToInt32(senId).Equals(cradleList[i].sensorList[j].id))
                            {
                                if (senEvent.Equals("30"))
                                {
                                    cradleList[i].sensorList[j].bleEnable = false;
                                }
                            }
                        }
                    }
                }
            }

            // Version [1000]
            if (response[0] == 0x10 && response[1] == 0x00)
            {
                // 1000 00FD 30 01 06 00 FFFF00100030010101000000
                string hw = protocol.Substring(8, 2);
                string fw_5 = protocol.Substring(10, 2);
                string fw_6 = protocol.Substring(12, 2);
                string fw_7 = protocol.Substring(14, 2);

                for (int i = 0; i < cradleList.Count; i++)
                {
                    if (mac.Equals(cradleList[i].mac))
                    {
                        if (hw.Equals("00"))
                            cradleList[i].VerHW = "EVT1";
                        else if (hw.Equals("01"))
                            cradleList[i].VerHW = "EVT2";
                        else if (hw.Equals("10"))
                            cradleList[i].VerHW = "DVT1";
                        else if (hw.Equals("20"))
                            cradleList[i].VerHW = "PVT1";
                        else if (hw.Equals("30"))
                            cradleList[i].VerHW = "preMP";
                        else if (hw.Equals("40"))
                            cradleList[i].VerHW = "MP";
                        cradleList[i].VerFW = fw_5.Replace("0", "") + "." + fw_6.Replace("0", "") + "." + fw_7.Replace("0", "");
                        cradleList[i].id = id;
                        cradleList[i].protocol = protocol;
                    }
                }
            }

            // BatteryInfo [5400]
            if (response[0] == 0x54 && response[1] == 0x00)
            {
                for (int i = 0; i < cradleList.Count; i++)
                {
                    if (mac.Equals(cradleList[i].mac))
                    {
                        cradleList[i].protocol = protocol;
                        cradleList[i].id = id;
                        // Convert hex to String
                        string Battery = protocol.Substring(12, 2);
                        cradleList[i].battery = Convert.ToInt32(Battery, 16);
                    }
                }
            }

            // BLE [600000]
            if (response[0] == 0x60 && response[1] == 0x00)
            {
                string enableStatus = protocol.Substring(4, 2);
                //Debug.Log("BLE Add data " + mac);
                if (enableStatus.Equals("00"))
                {
                    if (cradleList.Count == 0)
                    {
                        cradleList.Add(new CradleStatue(mac));
                        cradleList[0].id = id;
                        cradleList[0].protocol = protocol;
                        cradleList[0].status = "BLE";
                        cradleList[0].bleEnable = true;
                    }
                    else
                    {
                        bool exists = cradleList.Exists(x => x.mac == mac);
                        if (!exists)
                        {
                            cradleList.Add(new CradleStatue(mac));
                            int index = cradleList.FindIndex(x => x.mac == mac);
                            cradleList[index].id = id;
                            cradleList[index].protocol = protocol;
                            cradleList[index].status = "BLE";
                            cradleList[index].bleEnable = true;
                        }
                        else
                        {
                            for (int i = 0; i < cradleList.Count; i++)
                            {
                                if (mac == cradleList[i].mac)
                                {
                                    cradleList[i].id = id;
                                    cradleList[i].protocol = protocol;
                                    cradleList[i].status = "BLE";
                                    cradleList[i].bleEnable = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < cradleList.Count; i++)
                    {
                        if (mac == cradleList[i].mac)
                        {
                            cradleList[i].bleEnable = false;
                        }
                    }
                }
            }

            // WIFI [660013]
            if (response[0] == 0x66 && response[1] == 0x00)
            {
                //Debug.Log("Add data " + mac);
                if (cradleList.Count == 0)
                {
                    cradleList.Add(new CradleStatue(mac));
                    cradleList[0].id = id;
                    cradleList[0].protocol = protocol;
                    cradleList[0].status = "WIFI";
                }
                else
                {
                    bool exists = cradleList.Exists(x => x.mac == mac);
                    if (!exists)
                    {
                        cradleList.Add(new CradleStatue(mac));
                        int index = cradleList.FindIndex(x => x.mac == mac);
                        cradleList[index].id = id;
                        cradleList[index].protocol = protocol;
                        cradleList[index].status = "WIFI";
                    }
                    else
                    {
                        for (int i = 0; i < cradleList.Count; i++)
                        {
                            if (mac == cradleList[i].mac)
                            {
                                cradleList[i].id = id;
                                cradleList[i].protocol = protocol;
                                cradleList[i].status = "WIFI";
                            }
                        }
                    }
                }
            }
        }
    }

    private void PrintArray(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            print(i + " " + arr[i]);
        }
    }

    private byte[] StringToBytes(string s)
    {
        byte[] s_bytes = new byte[s.Length / 2];
        for (int i = 0; i < s.Length; i = i + 2)
        {
            //每2位16進位數字轉換為一個10進位整數
            s_bytes[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
        }
        return s_bytes;
    }

    private string BytesToString(byte[] bytes)
    {
        string result = "";

        foreach (var b in bytes)
            result += b.ToString("X2");

        return result;
    }
#endregion
}
