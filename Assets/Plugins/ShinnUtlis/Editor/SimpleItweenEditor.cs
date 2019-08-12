using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using Shinn;

[CustomEditor(typeof(SimpleItween)), CanEditMultipleObjects]
public class SimpleItweenEditor : Editor {

    //private SerializedProperty target;
    //private SerializedProperty time;
    //private SerializedProperty delay;
    //private SerializedProperty ease;
    //private SerializedProperty loop;

    //private SerializedProperty islocal;
    //private SerializedProperty ignoreTimeScalest;
    //private SerializedProperty AutoStart;
    //private SerializedProperty orienttopathst;
    //private SerializedProperty lookaheadValue;

    //private SerializedProperty startComplete;

    //GUIContent label_Target;
    //GUIContent label_Time;
    //GUIContent label_Delay;
    //GUIContent label_EaseType;
    //GUIContent label_Looptype;

    //GUIContent label_IsLocal;
    //GUIContent label_IgnoreTimeScalest;
    //GUIContent label_AutoStart;
    //GUIContent label_Orienttopathst;
    //GUIContent label_LookaheadValue;
    //GUIContent label_startComplete;

    ////private bool showItweenSettings = true;


    //private void OnEnable()
    //{
    //    target = serializedObject.FindProperty("target");
    //    time = serializedObject.FindProperty("time");
    //    delay = serializedObject.FindProperty("delay");
    //    ease = serializedObject.FindProperty("ease");
    //    loop = serializedObject.FindProperty("loop");

    //    islocal = serializedObject.FindProperty("islocal");
    //    ignoreTimeScalest = serializedObject.FindProperty("ignoreTimeScalest");
    //    AutoStart = serializedObject.FindProperty("AutoStart");
    //    orienttopathst = serializedObject.FindProperty("orienttopathst");
    //    lookaheadValue = serializedObject.FindProperty("lookaheadValue");

    //    startComplete = serializedObject.FindProperty("startComplete");

    //    label_Target = new GUIContent("Target");
    //    label_Time = new GUIContent("Time");
    //    label_Delay = new GUIContent("Delay");
    //    label_EaseType = new GUIContent("Easetype");
    //    label_Looptype = new GUIContent("Looptype");

    //    label_IsLocal = new GUIContent("IsLocal");
    //    label_IgnoreTimeScalest = new GUIContent("IgnoreTimeScale");
    //    label_AutoStart = new GUIContent("AutoStart");
    //    label_Orienttopathst = new GUIContent("Orienttopath");
    //    label_LookaheadValue = new GUIContent("LookaheadValue");

    //    label_startComplete = new GUIContent("Complete Event");
    //}



    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();

    //    EditorGUILayout.PropertyField(target, label_Target);
    //    EditorGUILayout.Space();
    //    EditorGUILayout.PropertyField(time, label_Time);
    //    EditorGUILayout.Slider(delay, 0, 60, label_Delay);
    //    EditorGUILayout.Space();
    //    EditorGUILayout.PropertyField(ease, label_EaseType);
    //    EditorGUILayout.PropertyField(loop, label_Looptype);
    //    EditorGUILayout.Space();
    //    EditorGUILayout.PropertyField(islocal, label_IsLocal);
    //    EditorGUILayout.PropertyField(ignoreTimeScalest, label_IgnoreTimeScalest);
    //    EditorGUILayout.Space();
    //    EditorGUILayout.PropertyField(orienttopathst, label_Orienttopathst);
    //    EditorGUILayout.Slider(lookaheadValue, 0, 1, label_LookaheadValue);
    //    EditorGUILayout.Space();
    //    EditorGUILayout.PropertyField(AutoStart, label_AutoStart);

    //    EditorGUILayout.Space();
    //    EditorGUILayout.PropertyField(startComplete, label_startComplete);

    //    serializedObject.ApplyModifiedProperties();
    //}



    SimpleItween script;
    bool showItweenSettings = true;

    public override void OnInspectorGUI()
    {
        script = (SimpleItween)target;

        EditorGUILayout.Space();
        showItweenSettings = EditorGUILayout.Foldout(showItweenSettings, "Itween");
        if (showItweenSettings)
        {

            EditorGUILayout.LabelField("General setting.");

            EditorGUILayout.Space();
            script.target = (GameObject)EditorGUILayout.ObjectField("Target", script.target, typeof(GameObject), true);

            EditorGUILayout.Space();
            script.time = EditorGUILayout.FloatField("Time", script.time);
            script.delay = EditorGUILayout.Slider("Delay", script.delay, 0, 60);

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

            if (script.startComplete)
            {

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("-----------------------");

                script.EnableVoid = EditorGUILayout.Toggle("void events", script.EnableVoid);
                script.EnableBool = EditorGUILayout.Toggle("bool events", script.EnableBool);
                script.EnableInt = EditorGUILayout.Toggle("int events", script.EnableInt);
                script.EnableFloat = EditorGUILayout.Toggle("float events", script.EnableFloat);
                script.EnableFloatArray = EditorGUILayout.Toggle("float array events", script.EnableFloatArray);
                script.EnableVector3 = EditorGUILayout.Toggle("vector3 events", script.EnableVector3);
                script.EnableColor = EditorGUILayout.Toggle("color events", script.EnableColor);


                if (script.EnableBool)
                {
                    SerializedProperty onCheck = serializedObject.FindProperty("boolevents");
                    EditorGUILayout.PropertyField(onCheck);
                    script.boolvalue = EditorGUILayout.Toggle("Input bool value", script.boolvalue);
                }

                if (script.EnableInt)
                {
                    EditorGUILayout.Space();
                    SerializedProperty onCheck = serializedObject.FindProperty("intevents");
                    EditorGUILayout.PropertyField(onCheck);
                    script.intvalue = EditorGUILayout.IntField("Input int value", script.intvalue);
                }

                if (script.EnableFloat)
                {
                    EditorGUILayout.Space();
                    SerializedProperty onCheck = serializedObject.FindProperty("floatevents");
                    EditorGUILayout.PropertyField(onCheck);
                    script.floatvalue = EditorGUILayout.FloatField("Input float value", script.floatvalue);
                }

                if (script.EnableFloatArray)
                {
                    EditorGUILayout.Space();
                    SerializedProperty onCheck = serializedObject.FindProperty("floatarratevents");
                    EditorGUILayout.PropertyField(onCheck);

                    SerializedProperty property = serializedObject.FindProperty("floatarrayvalue");
                    EditorGUILayout.PropertyField(property, new GUIContent("Input floatarray value"), true);

                }

                if (script.EnableVector3)
                {
                    EditorGUILayout.Space();
                    SerializedProperty onCheck = serializedObject.FindProperty("vector3events");
                    EditorGUILayout.PropertyField(onCheck);

                    SerializedProperty property = serializedObject.FindProperty("vector3value");
                    EditorGUILayout.PropertyField(property, new GUIContent("Input vector3 value"), true);

                }

                if (script.EnableColor)
                {
                    EditorGUILayout.Space();
                    SerializedProperty onCheck = serializedObject.FindProperty("colorevents");
                    EditorGUILayout.PropertyField(onCheck);
                    script.colorvalue = EditorGUILayout.ColorField("Input color value", script.colorvalue);
                }

                if (script.EnableVoid)
                {
                    EditorGUILayout.Space();
                    SerializedProperty onCheck = serializedObject.FindProperty("voidevents");
                    EditorGUILayout.PropertyField(onCheck);
                }

                if (GUI.changed)
                    serializedObject.ApplyModifiedProperties();
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("-----------------------");
        script.mystate = (SimpleItween.State)EditorGUILayout.EnumPopup("Simple Itween Fuction", script.mystate);

        EditorGUILayout.Space();
        SelectType();
    }


    void SelectType()
    {
        switch (script.mystate)
        {

            case SimpleItween.State.colorTo:
                script.endColor = EditorGUILayout.ColorField("EndColor", script.endColor);
                break;

            case SimpleItween.State.moveTo:
                script.moveloc = (Transform)EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
                break;


            case SimpleItween.State.moveToPx:
                script.moveloc = (Transform)EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
                break;

            case SimpleItween.State.moveToPy:
                script.moveloc = (Transform)EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
                break;

            case SimpleItween.State.moveToPz:
                script.moveloc = (Transform)EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
                break;


            case SimpleItween.State.rotationTo:
                script.rotvalue = EditorGUILayout.Vector3Field("Euler angles", script.rotvalue);
                break;

            case SimpleItween.State.scaleTo:
                script.scaleValue = EditorGUILayout.Vector3Field("Scale value", script.scaleValue);
                break;

            case SimpleItween.State.shakePosition:
                script.shakePos = EditorGUILayout.Vector3Field("Shake value", script.shakePos);
                break;

            case SimpleItween.State.punchPosition:
                script.punchPos = EditorGUILayout.Vector3Field("Punch value", script.punchPos);
                break;

            case SimpleItween.State.SP_fadeTo:
                script.fadeStart = EditorGUILayout.Slider("Sprite fade start", script.fadeStart, 0, 1);
                script.fadeEnd = EditorGUILayout.Slider("Sprite fade end", script.fadeEnd, 0, 1);
                break;

            case SimpleItween.State.rotationToAndMoveTo:
                script.moveloc = (Transform)EditorGUILayout.ObjectField("Target loc", script.moveloc, typeof(Transform), true);
                break;

        }
    }

}
