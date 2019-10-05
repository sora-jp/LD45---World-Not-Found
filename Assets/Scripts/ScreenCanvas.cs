using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCanvas : MonoBehaviour
{
    protected VirtualScreen Screen { get; private set; }

    public void Link(VirtualScreen screen)
    {
        Screen = screen;
    }
}
