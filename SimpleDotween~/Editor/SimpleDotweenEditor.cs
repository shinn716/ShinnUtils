//
//  SimpleDotween.cs
//  SimpleDotween
//
//  Created by Shinn on 2022/1/25.
//  Copyright © 2021 Shinn. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleDotween)), CanEditMultipleObjects]
public class SimpleDotweenEditor : Editor
{
    private bool showMenu = true;

    private new SerializedProperty target;
    private SerializedProperty mystate;
    private SerializedProperty time;
    private SerializedProperty delay;
    private SerializedProperty ease;
    private SerializedProperty ignoreTimeScale;
    private SerializedProperty autorun;

    private SerializedProperty startComplete;

    private SerializedProperty looptimes;
    private SerializedProperty looptype;

    private SerializedProperty moveloc;
    private SerializedProperty punchValue;
    private SerializedProperty shakeValue;
    private SerializedProperty scaleValue;
    private SerializedProperty posValue;
    private SerializedProperty pos2Value;
    private SerializedProperty rotvalue;

    private SerializedProperty endColor;
    private SerializedProperty fadeStart;
    private SerializedProperty fadeEnd;
    private SerializedProperty onCompleteVoidEvents;


    GUIContent label_target;
    GUIContent label_myState;
    GUIContent label_time;
    GUIContent label_delay;
    GUIContent label_ease;
    GUIContent label_ignoreTimeScale;
    GUIContent label_autorun;

    GUIContent label_looptimes;
    GUIContent label_looptype;

    GUIContent label_moveloc;
    GUIContent label_punchValue;
    GUIContent label_shakeValue;
    GUIContent label_scaleValue;
    GUIContent label_posValue;
    GUIContent label_pos2Value;
    GUIContent label_rotvalue;

    GUIContent label_endColor;
    GUIContent label_fadeStart;
    GUIContent label_fadeEnd;
    GUIContent label_onCompleteVoidEvents;

    private void OnEnable()
    {

        target = serializedObject.FindProperty("target");
        mystate = serializedObject.FindProperty("mystate");
        time = serializedObject.FindProperty("time");
        delay = serializedObject.FindProperty("delay");
        ease = serializedObject.FindProperty("ease");
        ignoreTimeScale = serializedObject.FindProperty("ignoreTimeScale");
        autorun = serializedObject.FindProperty("autorun");

        startComplete = serializedObject.FindProperty("startComplete");

        looptimes = serializedObject.FindProperty("looptimes");
        looptype = serializedObject.FindProperty("looptype");


        moveloc = serializedObject.FindProperty("moveloc");
        punchValue = serializedObject.FindProperty("punchValue");
        shakeValue = serializedObject.FindProperty("shakeValue");
        scaleValue = serializedObject.FindProperty("scaleValue");
        posValue = serializedObject.FindProperty("posValue");
        pos2Value = serializedObject.FindProperty("pos2Value");
        rotvalue = serializedObject.FindProperty("rotvalue");


        endColor = serializedObject.FindProperty("endColor");
        fadeStart = serializedObject.FindProperty("fadeStart");
        fadeEnd = serializedObject.FindProperty("fadeEnd");
        onCompleteVoidEvents = serializedObject.FindProperty("onCompleteVoidEvents");



        label_myState = new GUIContent(" - myState");
        label_target = new GUIContent("Target");
        label_time = new GUIContent("Time");
        label_delay = new GUIContent("Delay");
        label_ease = new GUIContent("Easetype");
        label_ignoreTimeScale = new GUIContent("IgnoreTimeScale");
        label_autorun = new GUIContent("AutoStart");

        label_looptimes = new GUIContent("Looptimes");
        label_looptype = new GUIContent("Looptype");

        label_moveloc = new GUIContent(" - Move to target");
        label_punchValue = new GUIContent(" - Pounch position");
        label_shakeValue = new GUIContent(" - Shake position");
        label_scaleValue = new GUIContent(" - Scale to/from");

        label_posValue = new GUIContent(" - Move to position");
        label_pos2Value = new GUIContent(" - Move to position 2D");
        label_rotvalue = new GUIContent(" - Rotate to");


        label_endColor = new GUIContent(" - End color");
        label_fadeStart = new GUIContent(" - Fade start");
        label_fadeEnd = new GUIContent(" - Fade end");
        label_onCompleteVoidEvents = new GUIContent("Complete Event");
    }
    
   public override void OnInspectorGUI()
   {
       serializedObject.Update();

       EditorGUILayout.Space();
       EditorGUILayout.Space();
       showMenu = EditorGUILayout.Foldout(showMenu, "SimpleDoTween - DoTween inspector controller");
       EditorGUILayout.Space();

        if (showMenu)
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(target, label_target);

            EditorGUILayout.PropertyField(time, label_time);
            EditorGUILayout.Slider(delay, 0, 60, label_delay);

            EditorGUILayout.PropertyField(ease, label_ease);
            EditorGUILayout.PropertyField(looptimes, label_looptimes);
            EditorGUILayout.PropertyField(looptype, label_looptype);
            EditorGUILayout.PropertyField(ignoreTimeScale, label_ignoreTimeScale);
            EditorGUILayout.PropertyField(autorun, label_autorun);


            EditorGUILayout.PropertyField(startComplete, label_onCompleteVoidEvents);

            //EditorGUILayout.PropertyField(onCompleteVoidEvents, label_onCompleteVoidEvents);

            if (startComplete.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(onCompleteVoidEvents, label_onCompleteVoidEvents);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Type: ");

        EditorGUILayout.PropertyField(mystate, label_myState);
        SelectType(mystate.enumValueIndex);

        serializedObject.ApplyModifiedProperties();
   }

    
   private void SelectType(int index)
   {
        //Debug.Log(index);
       switch (index)
       {
           case 0:
               EditorGUILayout.PropertyField(shakeValue, label_shakeValue);
               break;
           case 1:
               EditorGUILayout.PropertyField(punchValue, label_punchValue);
               break;
            case 2:
                EditorGUILayout.PropertyField(scaleValue, label_scaleValue);
                break;
            case 3:
                EditorGUILayout.PropertyField(scaleValue, label_scaleValue);
                break;
            case 4:
                EditorGUILayout.PropertyField(moveloc, label_moveloc);
                break;
            case 5:
                EditorGUILayout.PropertyField(moveloc, label_moveloc);
                break;
            case 6:
                EditorGUILayout.PropertyField(posValue, label_posValue);
                break;
            case 7:
                EditorGUILayout.PropertyField(pos2Value, label_pos2Value);
                break;
            case 8:
                EditorGUILayout.PropertyField(rotvalue, label_rotvalue);
                break;
            case 9:
                EditorGUILayout.PropertyField(rotvalue, label_rotvalue);
                break;
            case 10:
                EditorGUILayout.PropertyField(fadeStart, label_fadeStart);
                EditorGUILayout.PropertyField(fadeEnd, label_fadeEnd);
                break;
            case 11:
                EditorGUILayout.PropertyField(endColor, label_endColor);
                break;
            case 12:
                EditorGUILayout.PropertyField(endColor, label_endColor);
                break;
        }
    }

   private void ListIterator(string listName)
   {
       //List object
       SerializedProperty listIterator = serializedObject.FindProperty(listName);
       Rect drawZone = GUILayoutUtility.GetRect(0f, 16f);
       bool showChildren = EditorGUI.PropertyField(drawZone, listIterator);
       listIterator.NextVisible(showChildren);

       //List size
       drawZone = GUILayoutUtility.GetRect(0f, 16f);
       showChildren = EditorGUI.PropertyField(drawZone, listIterator);
       bool toBeContinued = listIterator.NextVisible(showChildren);
       //Elements
       int listElement = 0;
       while (toBeContinued)
       {
           drawZone = GUILayoutUtility.GetRect(0f, 16f);
           showChildren = EditorGUI.PropertyField(drawZone, listIterator);
           toBeContinued = listIterator.NextVisible(showChildren);
           listElement++;
       }
   }
   

}
