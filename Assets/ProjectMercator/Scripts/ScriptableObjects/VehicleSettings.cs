using UnityEngine;

[CreateAssetMenu(fileName = "VehicleSettings", menuName = "Scriptable Objects/VehicleSettings")]
public class VehicleSettings : ScriptableObject
{
    [Header("Давление и его настройки")]
    public int PressureMax = 0;
    public int PressureMaxDeadZone = 0;
    public int PressureMinDeadZone = 0;
    public int PressureBuildupRate = 0;
    public int PressureStartingNumber = 0;
    [Header("Температура и её настройки")]
    public float TemperatureMax = 0;
    public float TemperatureMin = 0;
    public float TemperatureMaxDeadZone = 0;
    public float TemperatureMinDeadZone = 0;
    public float TemperatureBuildupRate = 0;
    [Header("Ввод координат и его настройки")]
    public int CoordinateInputFrequency = 0;
    [Header("Уборка снега и его настройки")]
     
    [Header("Модификатор сложности механик")]
    public int DifficultMultiply = 0;
    
}
