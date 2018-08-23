using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomMaterialCon))]
public class CustomMaterialConEditor : Editor {

    CustomMaterialCon script;


    public override void OnInspectorGUI()
    {
        script = (CustomMaterialCon)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Custom value for material.");

        EditorGUILayout.Space();
        script.mat = (Material) EditorGUILayout.ObjectField("Material", script.mat, typeof(Material), true);
        script.priority = EditorGUILayout.TextField("Priority", script.priority);
        script.autoStart = EditorGUILayout.Toggle("Autostart", script.autoStart);
        
        EditorGUILayout.Space();
        script.time = EditorGUILayout.FloatField("Time", script.time);
        script.delay = EditorGUILayout.Slider("Delay", script.delay, 0, 10);

        EditorGUILayout.Space();
        script.ease = (iTween.EaseType)EditorGUILayout.EnumPopup("EaseType", script.ease);
        script.loop = (iTween.LoopType)EditorGUILayout.EnumPopup("LoopType", script.loop);

        EditorGUILayout.Space();
        script.ignoreTimeScalest = EditorGUILayout.Toggle("Ignore time scale", script.ignoreTimeScalest);
        

        EditorGUILayout.Space();
        script.complete = EditorGUILayout.Toggle("Complete event", script.complete);


        if (script.complete)
        {
            SerializedProperty onCheck = serializedObject.FindProperty("unityevent");
            EditorGUIUtility.LookLikeControls();
            EditorGUILayout.PropertyField(onCheck);

            if (GUI.changed)
                serializedObject.ApplyModifiedProperties();
        }


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("-----------------------");
        
        script.type = (CustomMaterialCon.state)EditorGUILayout.EnumPopup("Simple Itween Fuction", script.type);
        SelectType();
    }

    void SelectType()
    {
        switch (script.type)
        {
            default:
                break;

            case CustomMaterialCon.state.ColorControl:
                script.startColor = EditorGUILayout.ColorField("Start color", script.startColor);
                script.endColor = EditorGUILayout.ColorField("End color", script.endColor);
                break;


            case CustomMaterialCon.state.ValueControl:
                script.range = EditorGUILayout.Vector2Field("Range", script.range);
                break;
        }
    }



}
