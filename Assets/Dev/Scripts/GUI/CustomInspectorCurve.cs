using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Curve))]
public class CustomInspectorCurve : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Curve curve = (Curve)target;

        if (GUILayout.Button("Generate Pattoun"))
        {
            curve.GeneratePattoun(); 
        }

        /*if (GUILayout.Button("Add Generate Pattoun"))
        {
            curve.AddGeneratePattoun();
        }*/

        if (GUILayout.Button("Clear Pattoun"))
        {
            curve.Clear();
        }
    }
}
