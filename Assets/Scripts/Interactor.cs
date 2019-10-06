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
    public bool CanInteract => interactable?.CanInteract ?? selectable?.CanSelect ?? false;

    private IInteractable interactable;
    private IInteractable lastInteractable;
    private ISelectable selectable;
    private bool lastHover;
    private bool lastCanInteract;

    void Update()
    {
        selectable = null;
        interactable = null;
        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, 5))
        {
            selectable = hitInfo.transform.GetComponentInParent<ISelectable>();
            interactable = hitInfo.transform.GetComponentInParent<IInteractable>();
        }

        if (interactable != null)
        {
            if (interactable != lastInteractable)
            {
                interactable.OnMouseIn();
                lastInteractable?.OnMouseOut();
            }
            if (Input.GetMouseButtonDown(0) && interactable.CanInteract) interactable.Interact();
        }
        else
        {
            lastInteractable?.OnMouseOut();
            if (selectable != null && selectable != CurSelection && Input.GetMouseButtonDown(0) && selectable.CanSelect)
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
        }

        lastInteractable = interactable;

        Hovering = (interactable != null || selectable != null);
        HoverUpdated = Hovering != lastHover || lastCanInteract != CanInteract;
        lastHover = Hovering;
        lastCanInteract = CanInteract;


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
