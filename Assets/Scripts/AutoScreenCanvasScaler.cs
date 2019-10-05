using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScreenCanvasScaler : MonoBehaviour
{
    void Awake()
    {
        Mesh m = GetComponentInParent<MeshFilter>().mesh;
        float da = Vector3.Distance(m.vertices[0], m.vertices[1]);
        float db = Vector3.Distance(m.vertices[1], m.vertices[2]);

        float d = Mathf.Max(da, db);
        transform.localScale = Vector3.one * (d / 1920) * 2;
    }
}
