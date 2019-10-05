using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(VirtualScreen))]
public class AutoScreen : MonoBehaviour
{
    public ScreenCanvas canvasPrefab;
    public Transform canvas;

    private VirtualScreen m_screen;

    void Awake()
    {
        m_screen = GetComponent<VirtualScreen>();

        var terminal = Instantiate(canvasPrefab, canvas);
        terminal.Link(m_screen);
    }
}
