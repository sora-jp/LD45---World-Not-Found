using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PowerMinigame : Minigame
{
    private Connection[] connections;
    public CinemachineVirtualCamera vcam;
    public SwingDoor door;

    private void Awake()
    {
        HideMouse = false;
        connections = GetComponentsInChildren<Connection>();
    }

    protected override void Update()
    {
        base.Update();
        CanSelect = door.isOpen;
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

            if (c.type == ConnectionType.End && !c.isFinished) win = false;
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
