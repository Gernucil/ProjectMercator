using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Interact : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _mouseDistance;
    [SerializeField] private LayerMask _layerMaskBaseInteraction;
    [SerializeField] private LayerMask _layerMaskMouseInteraction;
    [SerializeField] private Vector2 _mouseDelta;
    private Camera _camera;
    public static Action<bool> CancelInteraction;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    public void PerformInteract()
    {
        Ray ray = _camera.ScreenPointToRay(_camera.ViewportToScreenPoint(new(0.5f, 0.5f, 0f)));
        Physics.Raycast(ray.origin, ray.direction * _distance, out RaycastHit hitInfo, _distance, _layerMaskBaseInteraction, QueryTriggerInteraction.Collide);

        if (hitInfo.collider != null)
        {
            if (!hitInfo.collider.TryGetComponent(out IInteractable interact))
                return;
            else interact.Interact(gameObject);
        }
        else return;
    }
    public void CancelInteract(bool performed)
    {
        CancelInteraction?.Invoke(performed);
    }

    public void MousePerformInteract()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Vector3 screenPositionClicked = Mouse.current.position.value;
            Ray ray = _camera.ScreenPointToRay(screenPositionClicked);
            Physics.Raycast(ray.origin, ray.direction * _mouseDistance, out RaycastHit hitInfo, _distance, _layerMaskMouseInteraction, QueryTriggerInteraction.Collide);

            if (hitInfo.collider != null)
            {
                if (!hitInfo.collider.TryGetComponent(out IMouseInteractable interact))
                    return;
                else interact.MouseInteract(_mouseDelta);
            }
            else return;
        }
    }
    public void MouseMoveDelta(Vector2 mouseDelta)
    {
        _mouseDelta = mouseDelta;
    }



#if UNITY_EDITOR
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        if (_camera == null)
        {
            _camera = GetComponentInChildren<Camera>();
        }
        Gizmos.color = Color.green;
        Ray ray = _camera.ScreenPointToRay(_camera.ViewportToScreenPoint(new(0.5f, 0.5f, 0f)));
        Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMaskBaseInteraction, QueryTriggerInteraction.Collide);
        Gizmos.DrawRay(ray.origin, ray.direction * _distance);
        Gizmos.DrawWireSphere(hitInfo.point, 0.1f);
    }
#endif
}
