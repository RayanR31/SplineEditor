using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CurveManager))]

public class CustomInspectorCurveManager : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CurveManager curveManager = (CurveManager)target;

        if (GUILayout.Button("Generate Pattoun"))
        {
            curveManager.GeneratePattoun();
        }


        if (GUILayout.Button("Clear Pattoun"))
        {
            curveManager.Clear();
        }


        if (GUILayout.Button("Add Point"))
        {
            curveManager.AddPoint();
        }
    }
}
