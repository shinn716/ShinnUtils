using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;

public class CheckMACaddress : MonoBehaviour {

    public static bool enable;
    public string Callip = "http://35.194.156.113/2018henson/time.php";
    public string[] MACaddress;
    

    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    string ServerAddress;

	public void Init () {

        if (enable)
        {
            print("enable " + enable);
            GetMACaddress();
            check();
        }
    }


    private void GetMACaddress()
    {
        List<string> macList = new List<string>();
        foreach (var nic in nics)
        {
            // 因為電腦中可能有很多的網卡(包含虛擬的網卡)，
            // 我只需要 Ethernet 網卡的 MAC
            if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                macList.Add(nic.GetPhysicalAddress().ToString());

                MACaddress = new string[macList.Count];

                for (int i = 0; i < macList.Count; i++)
                    MACaddress = macList.ToArray();
                
            }
        }
    }

    IEnumerator checktime()
    {
        using (WWW wwwResponse = new WWW(Callip))
        {
            /* wait for the download of the response to complete */
            yield return wwwResponse;
            /* display the content from the response */
            // print(wwwResponse.text);
            ServerAddress = wwwResponse.text;
            print("ServerAddress = " + ServerAddress);

            for (int i = 0; i < MACaddress.Length; i++)
            {
                if (ServerAddress != MACaddress[i])
                {
                    print("Failure...");
                    UnityEngine.Application.Quit();
                }
                else {
                    print("Success!!!");
                }
            }


        }
    }

    private void check()
    {
        StartCoroutine(checktime());
        //systime = System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString();
    }


}
