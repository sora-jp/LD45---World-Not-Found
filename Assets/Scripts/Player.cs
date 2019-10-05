using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public FirstPersonController controller;

    void Update()
    {
        controller.enabled = Interactor.Instance.CurSelection?.BlocksInput != true;
    }
}
