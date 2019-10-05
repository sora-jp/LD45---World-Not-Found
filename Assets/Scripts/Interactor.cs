using System.Collections;
using System.Collections.Generic;
using UnityBase.Utils;
using UnityEngine;

public class Interactor : Singleton<Interactor>
{

    public ISelectable CurSelection { get; private set; }
    public bool SelectionUpdated { get; private set; }
    public bool Hovering { get; private set; }
    public bool HoverUpdated { get; private set; }

    private IInteractable lastInteractable;
    private bool lastHover;

    void Update()
    {
        ISelectable selectable = null;
        IInteractable interactable = null;
        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, 5))
        {
            selectable = hitInfo.transform.GetComponentInParent<ISelectable>();
            interactable = hitInfo.transform.GetComponentInParent<IInteractable>();
        }

        if (selectable != null && selectable != CurSelection && Input.GetMouseButtonDown(0))
        {
            CurSelection?.Deselect();
            selectable.Select();
            CurSelection = selectable;
            SelectionUpdated = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CurSelection?.Deselect();
            CurSelection = null;
            SelectionUpdated = true;
        }

        if (interactable != null)
        {
            if (interactable != lastInteractable)
            {
                interactable.OnMouseIn();
                lastInteractable?.OnMouseOut();
            }
            if (Input.GetMouseButtonDown(0)) interactable.Interact();
        }
        else
        {
            lastInteractable?.OnMouseOut();
        }

        lastInteractable = interactable;

        Hovering = (interactable != null || selectable != null);
        HoverUpdated = Hovering != lastHover;
        lastHover = Hovering;


        SetCursorLocked(CurSelection?.HideMouse != false);
    }

    void SetCursorLocked(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    void LateUpdate()
    {
        SelectionUpdated = false;
    }
}
