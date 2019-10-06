using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityBase.Animations;

public class MissionUI : MonoBehaviour
{
    public TextMeshProUGUI title, desc;
    public TextMeshProUGUI permanentTitle, permanentDesc;
    public CanvasGroup permanentGroup;

    void Awake()
    {
        MissionProgression.Instance.OnNewMission += OnNewMission;
    }

    void OnNewMission()
    {
        title.text = MissionProgression.Instance.currentMission?.title;
        desc.text = MissionProgression.Instance.currentMission?.desc;
        if (MissionProgression.Instance.currentMission != null)
        {
            StartCoroutine(Animate());
        }
    }

    void Update()
    {
        if (Interactor.Instance.SelectionUpdated)
        {
            permanentGroup.AnimateAlpha((Interactor.Instance.CurSelection?.BlocksInput == true) ? 0 : 1, 0.25f, EaseMode.EaseInOut);
        }
    }

    private IEnumerator Animate()
    {
        permanentGroup.AnimateAlpha(0, 0.25f, EaseMode.EaseInOut);

        yield return new WaitForSeconds(0.25f);
        this.AnimateRectTransformPivot(Vector2.up + Vector2.right * 0.5f, 0.5f, EaseMode.EaseOut);

        yield return new WaitForSeconds(1.5f);

        permanentTitle.text = MissionProgression.Instance.currentMission?.title;
        permanentDesc.text = MissionProgression.Instance.currentMission?.desc;

        this.AnimateRectTransformPivot(Vector2.right * 0.5f, 0.5f, EaseMode.EaseIn);

        yield return new WaitForSeconds(0.5f);
        permanentGroup.AnimateAlpha(1, 0.25f, EaseMode.EaseInOut);
    }
}
