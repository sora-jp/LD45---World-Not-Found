using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissionBehaviour
{
    void OnStart();
    bool CheckCompletion();
    void OnCompleted();
    void Link(MissionProgression progression);
}

public abstract class MissionBehaviour : IMissionBehaviour
{
    protected MissionProgression Progression { get; private set; }

    public abstract void OnStart();
    public abstract bool CheckCompletion();
    public abstract void OnCompleted();

    public void Link(MissionProgression progression)
    {
        Progression = progression;
    }
}

public class TestMissionBehaviour : MissionBehaviour
{
    public override void OnStart()
    {
        Debug.Log("Start");
    }

    public override bool CheckCompletion()
    {
        return MinigameTracker.Instance.GetMinigame<PowerMinigame>().Completed;
    }

    public override void OnCompleted()
    {
        Debug.Log("Complete");
    }
}