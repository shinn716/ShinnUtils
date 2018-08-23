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

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Itween general setting.");

        EditorGUILayout.Space();
        script.target = (GameObject) EditorGUILayout.ObjectField("Target", script.target, typeof(GameObject), true);

        EditorGUILayout.Space();
        script.time = EditorGUILayout.FloatField("Time", script.time);
        script.delay = EditorGUILayout.Slider("Delay", script.delay, 0, 10);

        EditorGUILayout.Space();
        script.ease = (iTween.EaseType)EditorGUILayout.EnumPopup("EaseType", script.ease);
        script.loop = (iTween.LoopType)EditorGUILayout.EnumPopup("LoopType", script.loop);

        EditorGUILayout.Space();
        script.islocal = EditorGUILayout.Toggle("Is local", script.islocal);
        script.ignoreTimeScalest = EditorGUILayout.Toggle("Ignore time scale", script.ignoreTimeScalest);

        EditorGUILayout.Space();
        script.orienttopathst = EditorGUILayout.Toggle("Orien to path", script.orienttopathst);
        script.lookaheadValue = EditorGUILayout.Slider("Look ahead value", script.lookaheadValue, 0, 1);

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
        script.mystate = (SimpleItween.state)EditorGUILayout.EnumPopup("Simple Itween Fuction", script.mystate);

        EditorGUILayout.Space();
        SelectType();


    }

    void SelectType()
    {
        switch (script.mystate)
        {

            case SimpleItween.state.colorto:
                script.endColor = EditorGUILayout.ColorField("EndColor", script.endColor);
                break;


            case SimpleItween.state.moveto:
                script.moveloc = (Transform) EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
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
                script.fadeStart = EditorGUILayout.Slider("Sprite fade start", script.fadeStart, 0, 1);
                script.fadeEnd = EditorGUILayout.Slider("Sprite fade end", script.fadeEnd, 0, 1);
                break;

        }
    }

}
