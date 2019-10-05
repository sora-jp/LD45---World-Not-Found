using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMinigame : Minigame
{
    private Connection[] connections;

    private void Awake()
    {
        HideMouse = false;
        connections = FindObjectsOfType<Connection>();
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
