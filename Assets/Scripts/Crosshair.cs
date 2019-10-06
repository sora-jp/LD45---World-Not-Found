using System;
using System.Collections;
using System.Collections.Generic;
using UnityBase.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private CanvasGroup m_group;
    private RectTransform m_transform;
    public float animTime;
    public EaseMode easeMode;

    private bool hide = false;

    void Awake()
    {
        m_group = GetComponent<CanvasGroup>();
        m_transform = GetComponent<RectTransform>();
    }

    void StopAllAnims()
    {
        var c = m_group.GetComponent<AnimationCoroutineContainer>();
        if (c != null) c.StopAllCoroutines();
        c = m_transform.GetComponent<AnimationCoroutineContainer>();
        if (c != null) c.StopAllCoroutines();
    }

    void Update()
    {
        if (Interactor.Instance.SelectionUpdated)
        {
            hide = Interactor.Instance.CurSelection?.BlocksInput == true;
            m_group.AnimateAlpha(hide ? 0 : 1, animTime, easeMode);
        }

        if (!Interactor.Instance.HoverUpdated) return;

        var h = Interactor.Instance.Hovering && Interactor.Instance.CanInteract;
        StopAllAnims();
        m_group.AnimateAlpha(hide ? 0 : (h ? 1 : 0.2f), animTime, easeMode);
        m_transform.AnimateLocalRotation(Quaternion.Euler(0, 0, h ? 90 : 0), animTime, easeMode);
        m_transform.AnimateSizeDelta(Vector2.one * (h ? 50 : 25), animTime, easeMode);
    }
}
