using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableMonoBehaviour : MonoBehaviour, ISelectable
{
    public bool IsSelected { get; private set; }

    public virtual void Select()
    {
        IsSelected = true;
    }

    public virtual void Deselect()
    {
        IsSelected = false;
    }

    protected virtual void Update()
    {
        if (IsSelected) SelectedUpdate();
    }

    protected virtual void SelectedUpdate()
    {

    }

    public virtual bool BlocksInput { get; set; } = true;
    public virtual bool HideMouse { get; set; } = true;
}