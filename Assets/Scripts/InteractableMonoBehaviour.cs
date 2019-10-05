using UnityEngine;

public class InteractableMonoBehaviour : MonoBehaviour, IInteractable
{
    public virtual void Interact() { }
    public virtual void OnMouseIn() { }
    public virtual void OnMouseOut() { }
}