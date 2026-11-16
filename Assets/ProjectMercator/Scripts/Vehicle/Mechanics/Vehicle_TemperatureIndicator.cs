using UnityEngine;

public class Vehicle_TemperatureIndicator : MonoBehaviour
{
    [SerializeField] private Renderer _indicator;
    [SerializeField] private MaterialPropertyBlock _block;
    [SerializeField] private float _pulsationFrequancy;
    [SerializeField] private float _intensity;
    
    private bool _enabled = false;
    private Color color = Color.red;
    private float time = 0;
    void Start()
    {
        Vehicle_TemperatureBuildup.Instance.IsCurrentTemperature += IndicateTemperature;
        _indicator = GetComponent<Renderer>();        
        _block = new MaterialPropertyBlock();
    }

    private void IndicateTemperature(float currentPressure)
    {
        _indicator.GetPropertyBlock(_block);
        
        time += Time.deltaTime;

        _block.SetColor("_EmissiveColor", color * PulseIntensity());
        _indicator.SetPropertyBlock(_block);
    }
    private float PulseIntensity()
    {
        float intensity = 0;
        if (time >= _pulsationFrequancy)
        {
            time = 0;
            _enabled = !_enabled;
            intensity = _enabled ? _intensity : 1;
        }        
        return intensity;
    }
}
