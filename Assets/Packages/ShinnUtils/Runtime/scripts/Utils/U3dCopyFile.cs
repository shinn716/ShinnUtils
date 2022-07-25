#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;
using UnityEditor;

[ExecuteInEditMode]
public class U3dCopyFile : MonoBehaviour
{
    public string sourcesPath = "C:\\";
    public string targetPath = "D:\\";

    [ReadOnly]
    public List<string> filelist = new List<string>();

    Dictionary<string, FileInfo> fileinfolist = new Dictionary<string, FileInfo>();

    [ContextMenu("GetAllFilesInSources")]
    public void GetAllFilesInSources()
    {
        filelist.Clear();
        fileinfolist.Clear();
        var info = new DirectoryInfo(sourcesPath);
        var fileInfo = info.GetFiles();
        foreach (var file in fileInfo)
        {
            filelist.Add(file.ToString());
            fileinfolist.Add(file.ToString(), file);
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        filelist.Clear();
        fileinfolist.Clear();
    }

    [ContextMenu("Dduplicate")]
    public void Dduplicate()
    {
        print("Start Process... please wait");
        Process();
    }

    async void Process(Action callback = null)
    {
        var task01 = Task.Run(() =>
        {
            print($"Copy files count: {fileinfolist.Count}");
            foreach (var i in fileinfolist)
            {
                string[] splitArray = i.Key.Split(char.Parse("\\"));
                var copyname = splitArray[splitArray.Length - 1];
                File.Copy(i.Key, Path.Combine(targetPath, copyname), true);
                print($"Finish Process: {i.Key}  {i.Value.Length/1024/1024} MB");
            }
        }
        );
        callback?.Invoke();
        await Task.Yield();
    }
}




#region ReadOnlyAttribute 
/// <summary>
/// Read Only attribute.
/// Attribute is use only to mark ReadOnly properties.
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute { }

/// <summary>
/// This class contain custom drawer for ReadOnly attribute.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    /// <summary>
    /// Unity method for drawing GUI in Editor
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Saving previous GUI enabled value
        var previousGUIState = GUI.enabled;
        // Disabling edit for property
        GUI.enabled = false;
        // Drawing Property
        EditorGUI.PropertyField(position, property, label);
        // Setting old GUI enabled value
        GUI.enabled = previousGUIState;
    }
}
#endregion

#endif