using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurvePointRenderer))]
public class BezierCurveInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BezierCurvePointRenderer BezierCurve = (BezierCurvePointRenderer)target;
        if (GUILayout.Button("Bake"))
        {
            BezierCurve.starRecoder();
        }
    }
}
