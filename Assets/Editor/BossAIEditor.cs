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
    bool foldout;
    BossAI bossAI;
    const int SPACE_DISTANCE = 15;
    const int ADD_BUTTON_WIDTH = 80;

    private void OnEnable()
    {
        // Get script reference
        bossAI = target as BossAI;

        // Fill dictionary with all element types
        for (int i = 0; i < Enum.GetValues(typeof(ElementMain.ElementType)).Length; i++)
        {
            if (!bossAI.elementStates.ContainsKey((ElementMain.ElementType)i))
            {
                bossAI.elementStates.Add((ElementMain.ElementType)i, new List<Tuple<BossAI.StateOptions, State>>());
            }
        }
    }

    public override void OnInspectorGUI()
    {
        // Base GUI drawing
        base.OnInspectorGUI();

        // Bool that make every state hide
        foldout = EditorGUILayout.Foldout(foldout, InspectorName(nameof(bossAI.elementStates)));
        try
        {
            if (foldout)
            {
                // Show per element the assigned states
                for (int i = 0; i < Enum.GetValues(typeof(ElementMain.ElementType)).Length; i++)
                {
                    ElementMain.ElementType currentElement = (ElementMain.ElementType)i;

                    // Add state button
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(15);
                        if (GUILayout.Button("Add state", GUILayout.Width(ADD_BUTTON_WIDTH)))
                        {
                            bossAI.elementStates[currentElement].Add(new Tuple<BossAI.StateOptions, State>(0, null));
                        }
                        GUILayout.Label(currentElement.ToString());
                    }
                    EditorGUILayout.EndHorizontal();

                    // Items from list
                    for (int j = bossAI.elementStates[currentElement].Count - 1; j >= 0; j--)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUILayout.Space(15);

                            bossAI.elementStates[currentElement][j] = new Tuple<BossAI.StateOptions, State>(
                                (BossAI.StateOptions)EditorGUILayout.EnumPopup(bossAI.elementStates[currentElement][j].Item1),
                                (State)EditorGUILayout.ObjectField(bossAI.elementStates[currentElement][j].Item2, typeof(State), true));

                            if (GUILayout.Button("Remove state"))
                            {
                                bossAI.elementStates[currentElement].RemoveAt(j);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    GUILayout.Space(SPACE_DISTANCE);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    string InspectorName(string name)
    {
        string titleCase = Regex.Replace(name, "[A-Z]", " $&"); // Add space before each uppercase char
        return titleCase.Substring(0, 1).ToUpper() + titleCase.Substring(1); // Make first char uppercase
    }
}
