using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScreenCanvasScaler : MonoBehaviour
{
    public float multiplier = 2;

    void Awake()
    {
        Mesh m = GetComponentInParent<MeshFilter>().mesh;
        float da = Vector3.Distance(m.vertices[0], m.vertices[1]);
        float db = Vector3.Distance(m.vertices[1], m.vertices[2]);
        float dc = Vector3.Distance(m.vertices[2], m.vertices[3]);

        List<float> ds = new List<float> {da, db, dc};
        ds.Sort();
        float d = ds[1];
        Debug.Log($"{da} - {db} - {dc} (d: {d})");
        transform.localScale = Vector3.one * (d / 1920) * multiplier;
    }
}
