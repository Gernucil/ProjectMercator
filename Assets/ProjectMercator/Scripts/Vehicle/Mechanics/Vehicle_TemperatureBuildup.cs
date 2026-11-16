using System;
using UnityEngine;

public class Vehicle_TemperatureBuildup : MonoBehaviour
{
    [SerializeField] private VehicleSettings _vehicleSettings;
    private float _temperatureMax;
    private float _currentTemperature;
    private float _temperatureMaxDeadZone;
    private float _pressureMinDeadZone;
    private float _difficultModifier;
    private float _temperatureBuildupRate;

    public static Vehicle_TemperatureBuildup Instance;
    public Action<float> IsCurrentTemperature;

    void Awake()
    {
        Instance = this;
        _vehicleSettings = (VehicleSettings)Resources.Load("VehicleSettings");
        _temperatureMax = _vehicleSettings.TemperatureMax;
        _temperatureMaxDeadZone = _vehicleSettings.TemperatureMaxDeadZone;
        _pressureMinDeadZone = _vehicleSettings.TemperatureMinDeadZone;
        _temperatureBuildupRate = _vehicleSettings.TemperatureBuildupRate;
        _difficultModifier = _vehicleSettings.DifficultMultiply;
        
        _currentTemperature = _temperatureMax / 2; // Задаём текущее давление, чтобы оно не находилось ни в одном из пределов
    }

    
    void Update()
    {
        CheckMaxTemperature();
        CheckMaxDeadZoneTemperature();
        CheckMinDeadZoneTemperature();        
        TemperatureBuildup();
    }

    private void TemperatureBuildup()
    {
        _currentTemperature += _temperatureBuildupRate * _difficultModifier * Time.deltaTime;
        
    }
    private void CheckMaxDeadZoneTemperature()
    {
        if (_currentTemperature > _temperatureMaxDeadZone)
        {
            IsCurrentTemperature?.Invoke(_currentTemperature);
        }
    }
    private void CheckMinDeadZoneTemperature()
    {
        if (_currentTemperature < _pressureMinDeadZone)
        {
            IsCurrentTemperature?.Invoke(_currentTemperature);
        }
    }
    private void CheckMaxTemperature()
    {
        if (_currentTemperature >= _temperatureMax)
        {
            Debug.Log("We are lose");
        }
    }
}
