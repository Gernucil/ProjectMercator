using System;
using UnityEngine;

public class Vehicle_PressureBuildup : MonoBehaviour
{
    [SerializeField] private VehicleSettings _vehicleSettings;
    private float _pressureMax;
    [SerializeField] private float _currentPresure;
    private float _pressureMaxDeadZone;
    private float _pressureMinDeadZone;
    private float _difficultModifier;
    private float _pressureBuildupRate;
    private float _pressureStartingNumber;
    private bool _isReleasePressure;
    
    public static Vehicle_PressureBuildup Instance;
    public Action<float> IsCurrentPressure;

    void Awake()
    {
        Instance = this;
        _vehicleSettings = (VehicleSettings)Resources.Load("VehicleSettings");
        _pressureMax = _vehicleSettings.PressureMax;
        _pressureMaxDeadZone = _vehicleSettings.PressureMaxDeadZone;
        _pressureMinDeadZone = _vehicleSettings.PressureMinDeadZone;
        _pressureBuildupRate = _vehicleSettings.PressureBuildupRate;
        _difficultModifier = _vehicleSettings.DifficultMultiply;
        _pressureStartingNumber = _vehicleSettings.PressureStartingNumber;

        _currentPresure = _pressureStartingNumber;

        Vehicle_Lever.PressureRelease += ReleasePressure;
    }

    
    void Update()
    {
        CheckMaxPressure();
        CheckMaxDeadZonePressure();
        CheckMinDeadZonePressure();        
        PressureBuildup();
    }

    private void PressureBuildup()
    {
        if (_isReleasePressure) _currentPresure -= _pressureBuildupRate * _difficultModifier * Time.deltaTime;
        if (_isReleasePressure == false) _currentPresure += _pressureBuildupRate * _difficultModifier * Time.deltaTime;        
    }

    private void ReleasePressure(bool isReleasePressure)
    {
        _isReleasePressure = isReleasePressure;
    }

    private void CheckMaxDeadZonePressure()
    {
        if (_currentPresure > _pressureMaxDeadZone)
        {
            IsCurrentPressure?.Invoke(_currentPresure);
        }
    }
    private void CheckMinDeadZonePressure()
    {
        if (_currentPresure < _pressureMinDeadZone)
        {
            IsCurrentPressure?.Invoke(_currentPresure);
        }
    }
    private void CheckMaxPressure()
    {
        if (_currentPresure >= _pressureMax)
        {
            Debug.Log("We are lose");
        }
    }
}
