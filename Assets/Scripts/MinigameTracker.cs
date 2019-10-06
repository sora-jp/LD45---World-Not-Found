using System;
using System.Collections;
using System.Collections.Generic;
using UnityBase.Utils;
using UnityEngine;

public class MinigameTracker : Singleton<MinigameTracker>
{
    private Dictionary<Type, IMinigame> _typeToMinigame = new Dictionary<Type, IMinigame>();

    protected override void Awake()
    {
        base.Awake();
        foreach (var minigame in FindObjectsOfType<Minigame>()) _typeToMinigame.Add(minigame.GetType(), minigame);
    }

    public void RegisterMinigame(IMinigame minigame)
    {
        _typeToMinigame.Add(minigame.GetType(), minigame);
    }

    public T GetMinigame<T>() where T : IMinigame
    {
        return (T) _typeToMinigame[typeof(T)];
    }
}
