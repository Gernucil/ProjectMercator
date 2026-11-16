using UnityEngine;

internal interface IMouseInteractable
{
    public void MouseEnter(Vector2 mousePositionEnter);
    public void MouseExit(Vector2 mousePositionExit);
    public void MouseInteract(Vector2 mouseDelta);
    
}