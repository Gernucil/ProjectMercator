using System;
using Unity.Burst;
using UnityEngine;

public class Vehicle_Lever : MonoBehaviour, IMouseInteractable
{
    [Header("Настройки рачага")] // Возможно стоит их вывести в ScriptableObject
    [SerializeField] private float sensitivity;
    [SerializeField] private float _minAngleActionCap;
    [SerializeField] private float _maxAngle;

    private float _currentAngle;
    private float _angleActionCap;

    public static event Action<bool> PressureRelease;
    private Vector3 _initialEulerAngles;
    
    public void MouseInteract(Vector2 delta)
    {        
        RotateLever(delta);
    }

    private void RotateLever(Vector2 currentMouseDelta)
    {
        _currentAngle += -currentMouseDelta.normalized.y * sensitivity;
        _currentAngle = Mathf.Clamp(_currentAngle, 0, _maxAngle);
        Quaternion targetRotation = Quaternion.Euler(_initialEulerAngles.x, _initialEulerAngles.y, _currentAngle);

        transform.rotation = targetRotation;
    }
    private void Update()
    {
        if (_currentAngle >= _angleActionCap - _minAngleActionCap)
        {
            PressureRelease?.Invoke(true);
        }
        else PressureRelease?.Invoke(false);
    }
    private void Awake()
    {
        _initialEulerAngles = transform.eulerAngles;
        _angleActionCap = 90f;
    }



    //методы из интерфейса IMouseInteractable, пока функционала нет по причине ненадобности
    public void MouseEnter(Vector2 mousePositionIn) { }
    public void MouseExit(Vector2 mousePositionOut) { }
}
