using System.Collections;
using System.Collections.Generic;
using UnityBase.Animations;
using UnityEngine;

public class SwingDoor : InteractableMonoBehaviour
{
    public float openRotation;

    public bool isOpen = false;

    public override void Interact()
    {
        base.Interact();
        transform.AnimateLocalRotation(Quaternion.Euler(-90, 0, openRotation), 0.75f, EaseMode.EaseOut);
        isOpen = true;
        CanInteract = false;
    }
}
