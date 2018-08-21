using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomEditor(typeof(SimpleItween))]
public class SimpleItweenEditor : Editor {
    
    SimpleItween script;

    public override void OnInspectorGUI()
    {
        script = (SimpleItween)target;

        script.mystate = (SimpleItween.state)EditorGUILayout.EnumPopup("Simple Itween Fuction", script.mystate);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Itween general setting.");
        script.time = EditorGUILayout.FloatField("Time", script.time);
        script.delay = EditorGUILayout.FloatField("Delay", script.delay);

        EditorGUILayout.Space();
        script.ease = (iTween.EaseType)EditorGUILayout.EnumPopup("EaseType", script.ease);
        script.loop = (iTween.LoopType)EditorGUILayout.EnumPopup("LoopType", script.loop);

        EditorGUILayout.Space();
        script.islocal = EditorGUILayout.Toggle("Is local", script.islocal);
        script.ignoreTimeScalest = EditorGUILayout.Toggle("Ignore time scalest", script.ignoreTimeScalest);

        EditorGUILayout.Space();
        script.orienttopathst = EditorGUILayout.Toggle("Orien to path", script.orienttopathst);
        script.lookaheadValue = EditorGUILayout.FloatField("Look ahead value", script.lookaheadValue);

        EditorGUILayout.Space();
        script.AutoStart = EditorGUILayout.Toggle("Auto start", script.AutoStart);

        EditorGUILayout.Space();
        script.startComplete = EditorGUILayout.Toggle("Complete event", script.startComplete);

        if (script.startComplete) {
            SerializedProperty onCheck = serializedObject.FindProperty("unityevent");
            EditorGUIUtility.LookLikeControls();
            EditorGUILayout.PropertyField(onCheck);

            if (GUI.changed)
                serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("-----------------------");
        SelectType();


    }

    void SelectType()
    {
        switch (script.mystate)
        {

            case SimpleItween.state.lightColorto:
                script.endColor = EditorGUILayout.ColorField("EndColor", script.endColor);
                break;


            case SimpleItween.state.moveto:
                script.moveloc = (Transform) EditorGUILayout.ObjectField(new GUIContent("Source1"), script.moveloc, typeof(Transform), true);
                break;


            case SimpleItween.state.rotationto:
                script.rotvalue = EditorGUILayout.Vector3Field("Euler angles", script.rotvalue);
                break;


            case SimpleItween.state.scaleto:
                script.scaleValue = EditorGUILayout.Vector3Field("Scale value", script.scaleValue);
                break;


            case SimpleItween.state.shakePosition:
                script.shakePos = EditorGUILayout.Vector3Field("Shake value", script.shakePos);
                break;


            case SimpleItween.state.SP_fadeto:
                script.fadeStart = EditorGUILayout.FloatField("Sprite fade start", script.fadeStart);
                script.fadeEnd = EditorGUILayout.FloatField("Sprite fade end", script.fadeEnd);
                break;

        }
    }

}
