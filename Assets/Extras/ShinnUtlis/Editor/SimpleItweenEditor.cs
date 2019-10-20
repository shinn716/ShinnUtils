//
//  SimpleItween.cs
//  SimpleItween
//  itween http://www.pixelplacement.com/itween/index.php
//
//  Created by Shinn on 2019/8/25.
//  Copyright © 2019 Shinn. All rights reserved.
//

using UnityEngine;
using UnityEditor;
using Shinn;

[CustomEditor(typeof(SimpleItween)), CanEditMultipleObjects]
public class SimpleItweenEditor : Editor
{
    private bool showItweenSettings = true;
    private bool showItweenPathSettings;

    private SerializedProperty myState;

    private SerializedProperty target;
    private SerializedProperty time;
    private SerializedProperty delay;
    private SerializedProperty ease;
    private SerializedProperty loop;

    private SerializedProperty islocal;
    private SerializedProperty ignoreTimeScalest;
    private SerializedProperty AutoStart;
    private SerializedProperty orienttopathst;
    private SerializedProperty lookaheadValue;

    private SerializedProperty startComplete;

    private SerializedProperty bool_voidEvt;
    private SerializedProperty bool_booleanEvt;
    private SerializedProperty bool_intEvt;
    private SerializedProperty bool_floatEvt;
    private SerializedProperty bool_floatArrayEvt;
    private SerializedProperty bool_vector3Evt;
    private SerializedProperty bool_colorEvt;

    private SerializedProperty voidEvt;
    private SerializedProperty booleanEvt;
    private SerializedProperty intEvt;
    private SerializedProperty floatEvt;
    private SerializedProperty floatArrayEvt;
    private SerializedProperty vector3Evt;
    private SerializedProperty colorEvt;

    private SerializedProperty boolvalue;
    private SerializedProperty intvalue;
    private SerializedProperty floatvalue;
    //private SerializedProperty floatarrayvalue;
    private SerializedProperty vector3value;
    private SerializedProperty colorvalue;

    //private SerializedProperty startColor;
    private SerializedProperty endColor;
    private SerializedProperty shakePos;
    private SerializedProperty punchPos;
    private SerializedProperty scaleValue;
    private SerializedProperty moveloc;
    private SerializedProperty rotvalue;
    private SerializedProperty fadeStart;
    private SerializedProperty fadeEnd;

    private SerializedProperty posVectValue;

    GUIContent label_myState;

    GUIContent label_Target;
    GUIContent label_Time;
    GUIContent label_Delay;
    GUIContent label_EaseType;
    GUIContent label_Looptype;

    GUIContent label_IsLocal;
    GUIContent label_IgnoreTimeScalest;
    GUIContent label_AutoStart;
    GUIContent label_Orienttopathst;
    GUIContent label_LookaheadValue;
    GUIContent label_startComplete;

    GUIContent label_bvoidEvt;
    GUIContent label_bboolEvt;
    GUIContent label_bintEvt;
    GUIContent label_bfloatEvt;
    GUIContent label_bfloatArrayEvt;
    GUIContent label_bvector3Evt;
    GUIContent label_bcolorEvt;

    GUIContent label_voidEvt;
    GUIContent label_boolEvt;
    GUIContent label_intEvt;
    GUIContent label_floatEvt;
    GUIContent label_floatArrayEvt;
    GUIContent label_vector3Evt;
    GUIContent label_colorEvt;

    GUIContent label_boolvalue;
    GUIContent label_intvalue;
    GUIContent label_floatvalue;
    //GUIContent label_floatarrayvalue;
    GUIContent label_vector3value;
    GUIContent label_colorvalue;

    GUIContent label_startColor;
    GUIContent label_endColor;
    GUIContent label_shakePos;
    GUIContent label_punchPos;
    GUIContent label_scaleValue;
    GUIContent label_moveloc;
    GUIContent label_rotvalue;
    GUIContent label_fadeStart;
    GUIContent label_fadeEnd;

    GUIContent label_posVect;

    private void OnEnable()
    {
        myState = serializedObject.FindProperty("mystate");
        target = serializedObject.FindProperty("target");
        time = serializedObject.FindProperty("time");
        delay = serializedObject.FindProperty("delay");
        ease = serializedObject.FindProperty("ease");
        loop = serializedObject.FindProperty("loop");

        islocal = serializedObject.FindProperty("islocal");
        ignoreTimeScalest = serializedObject.FindProperty("ignoreTimeScalest");
        AutoStart = serializedObject.FindProperty("AutoStart");
        orienttopathst = serializedObject.FindProperty("orienttopathst");
        lookaheadValue = serializedObject.FindProperty("lookaheadValue");

        startComplete = serializedObject.FindProperty("startComplete");

        bool_voidEvt = serializedObject.FindProperty("EnableVoid");
        bool_intEvt = serializedObject.FindProperty("EnableInt");
        bool_floatEvt = serializedObject.FindProperty("EnableFloat");
        bool_floatArrayEvt = serializedObject.FindProperty("EnableFloatArray");
        bool_vector3Evt = serializedObject.FindProperty("EnableVector3");
        bool_colorEvt = serializedObject.FindProperty("EnableColor");
        bool_booleanEvt = serializedObject.FindProperty("EnableBool");

        voidEvt = serializedObject.FindProperty("voidevents");
        intEvt = serializedObject.FindProperty("intevents");
        floatEvt = serializedObject.FindProperty("floatevents");
        floatArrayEvt = serializedObject.FindProperty("floatarratevents");
        vector3Evt = serializedObject.FindProperty("vector3events");
        colorEvt = serializedObject.FindProperty("colorevents");
        booleanEvt = serializedObject.FindProperty("boolevents");

        boolvalue = serializedObject.FindProperty("boolvalue");
        intvalue = serializedObject.FindProperty("intvalue");
        floatvalue = serializedObject.FindProperty("floatvalue");
        //floatarrayvalue = serializedObject.FindProperty("floatarrayvalue");
        vector3value = serializedObject.FindProperty("vector3value");
        colorvalue = serializedObject.FindProperty("colorvalue");

        //startColor = serializedObject.FindProperty("startColor");
        endColor = serializedObject.FindProperty("endColor");
        shakePos = serializedObject.FindProperty("shakePos");
        punchPos = serializedObject.FindProperty("punchPos");
        scaleValue = serializedObject.FindProperty("scaleValue");
        moveloc = serializedObject.FindProperty("moveloc");
        rotvalue = serializedObject.FindProperty("rotvalue");
        fadeStart = serializedObject.FindProperty("fadeStart");
        fadeEnd = serializedObject.FindProperty("fadeEnd");

        posVectValue = serializedObject.FindProperty("posVect");


        label_myState = new GUIContent(" - myState");

        label_Target = new GUIContent("Target");
        label_Time = new GUIContent("Time");
        label_Delay = new GUIContent("Delay");
        label_EaseType = new GUIContent("Easetype");
        label_Looptype = new GUIContent("Looptype");

        label_IsLocal = new GUIContent("IsLocal");
        label_IgnoreTimeScalest = new GUIContent("IgnoreTimeScale");
        label_AutoStart = new GUIContent("AutoStart");
        label_Orienttopathst = new GUIContent("  - Orienttopath");
        label_LookaheadValue = new GUIContent("  - LookaheadValue");

        label_startComplete = new GUIContent("Complete Event");

        label_bvoidEvt = new GUIContent(" - Void Event");
        label_bboolEvt = new GUIContent(" - Bool Event");
        label_bintEvt = new GUIContent(" - Int Event");
        label_bfloatEvt = new GUIContent(" - Float Event");
        label_bfloatArrayEvt = new GUIContent(" - FloatArray Event");
        label_bvector3Evt = new GUIContent(" - Vector3 Event");
        label_bcolorEvt = new GUIContent(" - Color Event");

        label_voidEvt = new GUIContent("Void Event");
        label_boolEvt = new GUIContent("Bool Event");
        label_intEvt = new GUIContent("Int Event");
        label_floatEvt = new GUIContent("Float Event");
        label_floatArrayEvt = new GUIContent("FloatArray Event");
        label_vector3Evt = new GUIContent("Vector3 Event");
        label_colorEvt = new GUIContent("Color Event");

        label_startColor = new GUIContent(" - Start color");
        label_endColor = new GUIContent(" - End color");
        label_shakePos = new GUIContent(" - Shake position");
        label_punchPos = new GUIContent(" - Pounch position");
        label_scaleValue = new GUIContent(" - Scale to/from");
        label_moveloc = new GUIContent(" - Move/Rotate to obj/px/py/pz");
        label_rotvalue = new GUIContent(" - Rotation to");
        label_fadeStart = new GUIContent(" - Fade start");
        label_fadeEnd = new GUIContent(" - Fade end");

        label_boolvalue = new GUIContent(" - Set boolean value.");
        label_intvalue = new GUIContent(" - Set int value");
        label_floatvalue = new GUIContent(" - Set float value");
        //label_floatarrayvalue = new GUIContent("Set float array value");
        label_vector3value = new GUIContent(" - Set vector3 value");
        label_colorvalue = new GUIContent(" - Set color value");

        label_posVect = new GUIContent(" - Set position");
    }



    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        showItweenSettings = EditorGUILayout.Foldout(showItweenSettings, "SimpleiTween - Itween inspector controller");
        EditorGUILayout.Space();

        if (showItweenSettings)
        {
            showItweenPathSettings = EditorGUILayout.Foldout(showItweenPathSettings, "  Itween path function (Only be used for iTweenPath.)");
            if (showItweenPathSettings)
            {
                EditorGUILayout.PropertyField(orienttopathst, label_Orienttopathst);
                EditorGUILayout.Slider(lookaheadValue, 0, 1, label_LookaheadValue);
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(target, label_Target);

            EditorGUILayout.PropertyField(time, label_Time);
            EditorGUILayout.Slider(delay, 0, 60, label_Delay);

            EditorGUILayout.PropertyField(ease, label_EaseType);
            EditorGUILayout.PropertyField(loop, label_Looptype);

            EditorGUILayout.PropertyField(ignoreTimeScalest, label_IgnoreTimeScalest);

            EditorGUILayout.PropertyField(islocal, label_IsLocal);

            EditorGUILayout.PropertyField(AutoStart, label_AutoStart);

            EditorGUILayout.PropertyField(startComplete, label_startComplete);

            if (startComplete.boolValue)
            {

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(bool_voidEvt, label_bvoidEvt);
                if (bool_voidEvt.boolValue)
                {
                    EditorGUILayout.PropertyField(voidEvt, label_voidEvt);
                    EditorGUILayout.Space();
                }
                EditorGUILayout.PropertyField(bool_intEvt, label_bintEvt);
                if (bool_intEvt.boolValue)
                {
                    EditorGUILayout.PropertyField(intEvt, label_intEvt);
                    EditorGUILayout.PropertyField(intvalue, label_intvalue);
                    EditorGUILayout.Space();
                }

                EditorGUILayout.PropertyField(bool_floatEvt, label_bfloatEvt);
                if (bool_floatEvt.boolValue)
                {
                    EditorGUILayout.PropertyField(floatEvt, label_floatEvt);
                    EditorGUILayout.PropertyField(floatvalue, label_floatvalue);
                    EditorGUILayout.Space();
                }

                //EditorGUILayout.PropertyField(bool_floatArrayEvt, label_bfloatArrayEvt);
                //if (bool_floatArrayEvt.boolValue)
                //{
                //    EditorGUILayout.PropertyField(floatArrayEvt, label_floatArrayEvt);
                //    ListIterator("floatarrayvalue");
                //    EditorGUILayout.Space();
                //}

                EditorGUILayout.PropertyField(bool_vector3Evt, label_bvector3Evt);
                if (bool_vector3Evt.boolValue)
                {
                    EditorGUILayout.PropertyField(vector3Evt, label_vector3Evt);
                    EditorGUILayout.PropertyField(vector3value, label_vector3value);
                    EditorGUILayout.Space();
                }

                EditorGUILayout.PropertyField(bool_colorEvt, label_bcolorEvt);
                if (bool_colorEvt.boolValue)
                {
                    EditorGUILayout.PropertyField(colorEvt, label_colorEvt);
                    EditorGUILayout.PropertyField(colorvalue, label_colorvalue);
                    EditorGUILayout.Space();
                }

                EditorGUILayout.PropertyField(bool_booleanEvt, label_bboolEvt);
                if (bool_booleanEvt.boolValue)
                {
                    EditorGUILayout.PropertyField(booleanEvt, label_boolEvt);
                    EditorGUILayout.PropertyField(boolvalue, label_boolvalue);
                    EditorGUILayout.Space();
                }
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Type: ");

        EditorGUILayout.PropertyField(myState, label_myState);
        SelectType(myState.enumValueIndex);

        serializedObject.ApplyModifiedProperties();
    }

    private void SelectType(int index)
    {
        switch (index)
        {
            case 0:
                EditorGUILayout.PropertyField(shakePos, label_shakePos);
                break;
            case 1:
                EditorGUILayout.PropertyField(punchPos, label_punchPos);
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
                EditorGUILayout.PropertyField(moveloc, label_moveloc);
                break;
            case 7:
                EditorGUILayout.PropertyField(moveloc, label_moveloc);
                break;

            case 8:
                EditorGUILayout.PropertyField(rotvalue, label_rotvalue);
                break;

            case 9:
                EditorGUILayout.PropertyField(fadeStart, label_fadeStart);
                EditorGUILayout.PropertyField(fadeEnd, label_fadeEnd);
                break;


            case 10:
                //EditorGUILayout.PropertyField(startColor, label_startColor);
                EditorGUILayout.PropertyField(endColor, label_endColor);
                break;

            case 11:
                EditorGUILayout.PropertyField(moveloc, label_moveloc);
                break;

            case 12:
                EditorGUILayout.PropertyField(posVectValue, label_posVect);
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
