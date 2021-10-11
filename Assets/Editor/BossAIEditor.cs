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

        foldout = EditorGUILayout.Foldout(foldout, InspectorName(nameof(bossAI.dictionary)));
        try
        {
            if (foldout)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    values[i] = (BossAI.StateOptions)EditorGUILayout.EnumPopup(values[i]);
                    keys[i] = (State)EditorGUILayout.ObjectField(keys[i], typeof(State), true);
                    EditorGUILayout.EndHorizontal();
                }


                if (GUILayout.Button("Add"))
                {
                    keys.Add(null);
                    values.Add(0);
                    Debug.Log("added");
                }
            }

            for (int i = 0; i < Enum.GetValues(typeof(ElementMain.ElementType)).Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Space(15);
                    if (GUILayout.Button("Add state", GUILayout.Width(80)))
                    {
                        bossAI.elementStates.Add(new Tuple<State, BossAI.StateOptions>(null, 0));
                    }
                    GUILayout.Label(((ElementMain.ElementType)i).ToString());
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(15);

                for (int j = 0; j < bossAI.elementStates.Count; j++)
                {
                    Debug.Log(bossAI.elementStates.Count);
                    EditorGUILayout.BeginHorizontal();
                    bossAI.elementStates[j] = new Tuple<State, BossAI.StateOptions>(
                        (State)EditorGUILayout.ObjectField(bossAI.elementStates[j].Item1, typeof(State), true),
                        (BossAI.StateOptions)EditorGUILayout.EnumPopup(bossAI.elementStates[j].Item2));
                    EditorGUILayout.EndHorizontal();
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

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
