using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
public class BezierCurvePointRenderer : MonoBehaviour
{
#if UNITY_EDITOR
    private LineRenderer lineRenderer;
    [Range(1, 999)]
    public int vertexCount = 1;
    [Range(0.01f, 0.5f)]
    public float lineWidth = 0.02f;
    public bool showRendererMask;

    private List<Transform> positions;

    private List<Vector3> pointList;
    private Transform pointPath;
    private void Start()
    {
        positions = new List<Transform>();
        pointList = new List<Vector3>();
        lineRenderer = GameObject.Find("Line").GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        pointPath = transform.Find("pointList");
    }
    private void Update()
    {
        positions.Clear();
        for (int i = 0; i < pointPath.childCount; i++)
        {
            positions.Add(pointPath.GetChild(i));
        }

        if (positions.Count < 3)
        {
            return;
        }
        if (lookingTarget != null)
            animTarget.transform.LookAt(lookingTarget);
        BezierCurveWithUnlimitPoints();
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
        lineRenderer.widthMultiplier = lineWidth;
    }

    public void BezierCurveWithUnlimitPoints()
    {
        pointList.Clear();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            pointList.Add(UnlimitBezierCurve(positions, ratio));
        }
        pointList.Add(positions[positions.Count - 1].position);
    }

    public Vector3 UnlimitBezierCurve(List<Transform> trans, float t)
    {
        Vector3[] temp = new Vector3[trans.Count];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = trans[i].position;
        }
        int n = temp.Length - 1;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n - i; j++)
            {
                temp[j] = Vector3.Lerp(temp[j], temp[j + 1], t);
            }
        }
        return temp[0];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (positions.Count < 2)
            return;
        for (int i = 0; i < positions.Count - 1; i++)
        {
            Gizmos.DrawLine(positions[i].position, positions[i + 1].position);
        }
        if (showRendererMask)
        {
            Gizmos.color = Color.red;
            Vector3[] temp = new Vector3[positions.Count];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = positions[i].position;
            }
            int n = temp.Length - 1;
            for (float ratio = 0.5f / vertexCount; ratio < 1; ratio += 1.0f / vertexCount)
            {
                for (int i = 0; i < n - 2; i++)
                {
                    Gizmos.DrawLine(Vector3.Lerp(temp[i], temp[i + 1], ratio), Vector3.Lerp(temp[i + 2], temp[i + 3], ratio));
                }

            }
        }
    }




    public Transform animTarget;
    public Transform lookingTarget;
    [Range(0.1f, 200f)]
    public float speed = 10f;
    public void TargetTween(int i)
    {
        if (i == 0)
        {
            animTarget.transform.position = pointList[0];
            i++;
        }

        animTarget.transform.DOMove(pointList[i], 1 / speed).OnComplete(() =>
          {
              i++;
              if (i >= pointList.Count)
              {
                  record = false;
              }
              else
              {

                  TargetTween(i);
              }
          });
    }



    private AnimationClip clip;
    private bool record = false;
    private UnityEditor.Experimental.Animations.GameObjectRecorder _recorder;
    public string filePath = "Assets/Test/111";
    [Range(0.1f, 1)]
    public float Keyframe = 0.5f;
    public bool recursionChild = false;
    public void starRecoder()
    {
        if (animTarget == null)
        {
            Debug.LogError("Animation Target is Null");
            return;
        }
        clip = new AnimationClip();
        _recorder = new UnityEditor.Experimental.Animations.GameObjectRecorder();
        _recorder.root = animTarget.gameObject;
        _recorder.BindComponent<Transform>(animTarget.gameObject, recursionChild);
        record = true;
        TargetTween(0);
    }

    void LateUpdate()
    {
        if (clip == null)
            return;
        if (record)
        {
            _recorder.TakeSnapshot(Time.deltaTime * Keyframe);
        }
        else if (_recorder.isRecording)
        {
            _recorder.SaveToClip(clip);
            _recorder.ResetRecording();
            UnityEditor.AssetDatabase.CreateAsset(clip, filePath + ".anim");
            Debug.Log("Save Succeed");
        }
    }
#endif
}

