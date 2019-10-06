using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HdrImage : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color color;

    private Image img;

    void Awake()
    {
        OnValidate();
    }

    void OnValidate()
    {
        if (img == null) img = GetComponent<Image>();
        if (img == null) return;
        img.color = color;
    }
}
