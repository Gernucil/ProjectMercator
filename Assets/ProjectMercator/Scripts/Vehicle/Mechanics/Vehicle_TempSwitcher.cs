using UnityEngine;

public class Vehicle_TempSwitcher : MonoBehaviour, IMouseInteractable
{
    [SerializeField] private Transform _zeroDistanceLimiter;
    [SerializeField] private Transform _firstDistanceLimiter;
    [SerializeField] private Transform _regulator;
    [SerializeField] private float _sensivity;
    
    public void MouseInteract(Vector2 mouseDelta)
    {
        float currentPosition = mouseDelta.normalized.x * _sensivity;
        float targetPosition = Mathf.Clamp(currentPosition, _zeroDistanceLimiter.position.x, _firstDistanceLimiter.position.x);

        _regulator.position.Set(targetPosition, transform.position.y, transform.position.z);
    }

    public void MouseEnter(Vector2 mousePositionEnter) { }
    public void MouseExit(Vector2 mousePositionExit) { }
}
