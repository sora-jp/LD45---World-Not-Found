using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualScreen : SelectableMonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    void Awake()
    {
        SetSelectionStatus(false);
    }

    public override void Select()
    {
        base.Select();
        SetSelectionStatus(true);
    }

    public override void Deselect()
    {
        base.Deselect();
        SetSelectionStatus(false);
    }

    void SetSelectionStatus(bool selected)
    {
        vCam.enabled = selected;
    }
}
