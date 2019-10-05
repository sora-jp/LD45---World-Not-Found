using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : SelectableMonoBehaviour, IMinigame
{
    private Action m_completionCallback;
    private bool m_complete = false;

    protected void CompleteMinigame()
    {
        m_completionCallback();
        m_complete = true;
    }

    public void SetCompletionCallback(Action onComplete)
    {
        m_completionCallback = onComplete;
    }

    protected override void Update()
    {
        base.Update();
        VisualUpdate();
    }

    protected override void SelectedUpdate()
    {
        if (m_complete) return;
        LogicUpdate();
    }

    public abstract void LogicUpdate();
    public abstract void VisualUpdate();
}
