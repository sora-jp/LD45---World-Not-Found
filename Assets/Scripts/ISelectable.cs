public interface ISelectable
{
    bool CanSelect { get; }
    bool BlocksInput { get; }
    bool HideMouse { get; }
    void Select();
    void Deselect();
}