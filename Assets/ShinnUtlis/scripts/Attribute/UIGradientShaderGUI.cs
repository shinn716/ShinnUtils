using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
 
// ReSharper disable once CheckNamespace
// ReSharper disable once InconsistentNaming
[UsedImplicitly]
public class UIGradientShaderGUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        base.OnGUI(materialEditor, properties);
     
        EditorGUILayout.Space();
 
        var material = materialEditor.target as Material;
        if (material == null)
            throw new ArgumentNullException(nameof(material));
 
        var colorKeys = Enumerable
            .Range(0, material.GetInt("_Colors"))
            .Select((s, t) => new GradientColorKey(material.GetColor($"_Color{t}"), material.GetFloat($"_ColorTime{t}")))
            .ToArray();
 
        var alphaKeys = Enumerable
            .Range(0, material.GetInt("_Alphas"))
            .Select((s, t) => new GradientAlphaKey(material.GetFloat($"_Alpha{t}"), material.GetFloat($"_AlphaTime{t}")))
            .ToArray();
 
        var gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);
 
        EditorGUI.BeginChangeCheck();
 
        var field = EditorGUILayout.GradientField("Gradient", gradient);
 
        if (!EditorGUI.EndChangeCheck())
            return;
 
        for (var i = 0; i < 8; i++)
        {
            material.SetColor($"_Color{i}", Color.magenta);
            material.SetFloat($"_ColorTime{i}", -1.0f);
            material.SetFloat($"_Alpha{i}", -1.0f);
            material.SetFloat($"_AlphaTime{i}", -1.0f);
        }
 
        for (var i = 0; i < field.colorKeys.Length; i++)
        {
            material.SetColor($"_Color{i}", field.colorKeys[i].color);
            material.SetFloat($"_ColorTime{i}", field.colorKeys[i].time);
        }
 
        for (var i = 0; i < field.alphaKeys.Length; i++)
        {
            material.SetFloat($"_Alpha{i}", field.alphaKeys[i].alpha);
            material.SetFloat($"_AlphaTime{i}", field.alphaKeys[i].time);
        }
 
        material.SetInt("_Colors", field.colorKeys.Length);
 
        material.SetInt("_Alphas", field.alphaKeys.Length);
 
        EditorUtility.SetDirty(material);
    }
}