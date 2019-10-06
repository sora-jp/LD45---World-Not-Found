using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityBase.Utils;
using UnityEngine;

public class MissionProgression : Singleton<MissionProgression>
{
    public Mission startMission;
    public float delay;
    [HideInInspector] public Mission currentMission;
    public event Action OnNewMission;
    private bool waiting = false;

    void Start()
    {
        StartCoroutine(BeginMission(startMission));
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!waiting && (currentMission?.behaviour?.CheckCompletion() == true || Input.GetKeyDown(KeyCode.P)))
#else
        if (!waiting && currentMission?.behaviour?.CheckCompletion() == true)
#endif
        {
            StartCoroutine(BeginMission(currentMission?.next));
        }
    }

    IEnumerator BeginMission(Mission m)
    {
        currentMission?.behaviour?.OnCompleted();
        currentMission = m;
        currentMission?.behaviour?.Link(this);
        yield return new WaitForSeconds((m?.delayToNext ?? 0) + delay);
        currentMission?.behaviour?.OnStart();
        OnNewMission?.Invoke();
    }
}
