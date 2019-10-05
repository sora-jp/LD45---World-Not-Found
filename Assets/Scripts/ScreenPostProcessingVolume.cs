using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityBase.Animations;

public class ScreenPostProcessingVolume : MonoBehaviour
{
    public float smoothTime;
    private PostProcessVolume m_volume;

    private float target, vel;

    void Start()
    {
        m_volume = GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        target = Interactor.Instance.CurSelection is VirtualScreen ? 1 : 0;
        m_volume.weight = Mathf.SmoothDamp(m_volume.weight, target, ref vel, smoothTime);
    }
}
