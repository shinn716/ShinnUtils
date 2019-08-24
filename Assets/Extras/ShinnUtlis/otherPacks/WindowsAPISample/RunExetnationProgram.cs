using System.Collections;
using UnityEngine;
using System;
using System.IO;

public class RunExtensionProgram : MonoBehaviour
{
    private readonly string configName = "ExtensionPath.txt";

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        try
        {
            string m_readPath = Path.Combine(Application.streamingAssetsPath, configName);

            try
            {
                string filalPath = Path.Combine(Environment.CurrentDirectory, File.ReadAllText(m_readPath));
                WindowsEventAPI.SetWindowEvent(filalPath, WindowsEventAPI.WindowsStyle.Minimized);
            }

            catch (Exception e)
            {
                Debug.LogError("Can't read  files. " + e);
            }
        }


        catch (Exception e)
        {
            Debug.LogError("Can't read ExetnationPath.txt " + e);
        }


    }
}
