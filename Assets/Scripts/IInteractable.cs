﻿public interface IInteractable
{
    bool CanInteract { get; }
    void Interact();
    void OnMouseIn();
    void OnMouseOut();
}