using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigame : ISelectable
{
    void LogicUpdate();
    void VisualUpdate();
    void SetCompletionCallback(Action onComplete);
}
