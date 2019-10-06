using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScreenSpaceCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = Camera.main.nearClipPlane + 0.01f;
    }
}
