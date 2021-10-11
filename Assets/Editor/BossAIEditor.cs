using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BossAI))]
public class BossAIEditor : Editor
{
    Dictionary<State, BossAI.StateOptions> dictionary;
    List<State> keys;
    List<BossAI.StateOptions> values;
    bool foldout;
    BossAI bossAI;

    BossAIEditor()
    {
        dictionary = new Dictionary<State, BossAI.StateOptions>();
        keys = new List<State>();
        values = new List<BossAI.StateOptions>();
        foreach (State key in dictionary.Keys)
        {
            keys.Add(key);
            values.Add(dictionary[key]);
        }
    }

    private void OnEnable()
    {
        bossAI = target as BossAI;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        dictionary = bossAI.dictionary;

        try
        {
            foldout = EditorGUILayout.Foldout(foldout, InspectorName(nameof(bossAI.dictionary)));
            if (foldout)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    values[i] = (BossAI.StateOptions)EditorGUILayout.EnumPopup(values[i]);
                    //EditorGUILayout.PropertyField(null);
                    keys[i] = (State)EditorGUILayout.ObjectField(keys[i], typeof(State), true);
                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add"))
                {
                    keys.Add(null);
                    values.Add(0);
                    Debug.Log("added");
                    //Debug.Log(dictionary.Count);
                }
            }
        }
        catch { }

        ((BossAI)target).dictionary = dictionary;
    }

    string InspectorName(string name)
    {
        string[] split = Regex.Split(name, @"[A-Z]");
        name = "";
        for (int i = 0; i < split.Length; i++)
        {
            name += $" {split[i]}";
        }
        return name.Substring(0, 2).ToUpper() + name.Substring(2);
    }
}
