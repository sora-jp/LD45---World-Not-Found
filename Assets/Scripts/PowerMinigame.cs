using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PowerMinigame : Minigame
{
    public Connection[] connections;
    public CinemachineVirtualCamera vcam;

    private void Awake()
    {
        HideMouse = false;
    }

    public override void Select()
    {
        base.Select();
        vcam.enabled = true;
    }

    public override void Deselect()
    {
        base.Deselect();
        vcam.enabled = false;
    }

    public override void LogicUpdate()
    {
        bool win = true;
        foreach (Connection c in connections)
        {
            c.LogicUpdate();

            if (!c.isFinished) win = false;
        }

        if (win) CompleteMinigame();
    }

    public override void VisualUpdate()
    {
        foreach (Connection c in connections)
        {
            c.VisualUpdate();
        }
    }
}
