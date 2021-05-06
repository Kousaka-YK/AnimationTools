using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BezierControlMeum : Editor
{

    [MenuItem("GameObject/TweenPathLine/ShowLine", priority = 20)]
    static void ShowLine()
    {
        CreateBezierControl();
    }

    private static void CreateBezierControl()
    {
        GameObject bezierControl = new GameObject();
        bezierControl.name = "bezierControl";
        GameObject line = GameObject.Instantiate(Resources.Load<GameObject>("Line"));
        line.name = "Line";
        line.transform.parent = bezierControl.transform;

        GameObject pointList = new GameObject();
        pointList.transform.parent = bezierControl.transform;
        pointList.name = "pointList";

        bezierControl.AddComponent<BezierCurvePointRenderer>();
    }
}
