using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class Mission : SerializedScriptableObject
{
    public string title;
    public string desc;
    public IMissionBehaviour behaviour;
    public Mission next;
    public float delayToNext;
}
