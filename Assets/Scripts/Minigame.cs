using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : SelectableMonoBehaviour, IMinigame
{
    private Action m_completionCallback;
    private bool m_complete = false;
    public bool Completed => m_complete;

    protected void CompleteMinigame()
    {
        if (m_complete) return;

        m_completionCallback?.Invoke();
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
