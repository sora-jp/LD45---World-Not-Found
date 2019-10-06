using UnityEngine;

public class InteractableMonoBehaviour : MonoBehaviour, IInteractable
{
    public bool CanInteract { get; set; } = true;
    public virtual void Interact() { }
    public virtual void OnMouseIn() { }
    public virtual void OnMouseOut() { }
}