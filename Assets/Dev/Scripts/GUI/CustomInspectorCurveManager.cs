using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[CustomEditor(typeof(CurveManager))]

public class CustomInspectorCurveManager : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ActionButton();
    }
    void OnGUI()
    {
       // GUILayout.Button("A Button with fixed width", GUILayout.Width(300));
    }
    void ActionButton()
    {
        CurveManager curveManager = (CurveManager)target;

        GUILayout.Space(10f);

        if (GUILayout.Button("Generate NEW GameObject", GUILayout.Height(30f)))
        {
            curveManager.GenerateGameObject();
        }

        GUILayout.Space(10f);

        if (GUILayout.Button("ADD GameObject", GUILayout.Height(30f)))
        {
            curveManager.AddGameObject();
        }

        GUILayout.Space(10f);

        if (GUILayout.Button("Clear GameObject", GUILayout.Height(30f)))
        {
            curveManager.Clear();
        }

        GUILayout.Space(10f);

        if (GUILayout.Button("Reset Curve", GUILayout.Height(30f)))
        {
            curveManager.ResetCurve();
        }

        GUILayout.Space(10f);

        if (GUILayout.Button("Add Point", GUILayout.Height(30f)))
        {
            curveManager.AddPoint();
        }

        GUILayout.Space(10f);

        if (GUILayout.Button("Delete Point", GUILayout.Height(30f)))
        {
            curveManager.DeletePoint();
        }
    }
}
