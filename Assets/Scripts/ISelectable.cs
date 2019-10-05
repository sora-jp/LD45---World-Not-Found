public interface ISelectable
{
    bool BlocksInput { get; }
    bool HideMouse { get; }
    void Select();
    void Deselect();
}