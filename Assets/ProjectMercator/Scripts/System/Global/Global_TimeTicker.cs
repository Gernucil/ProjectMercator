using System;
using UnityEngine;

public class Global_TimeTicker : MonoBehaviour
{
    private float _second;
    private float _msSecond;
    private const float TICK_MAX = 0.1f;
    public static event Action<float> OneSecondTick;
    public static event Action<float> MilisecondTick;
    public static event Action UpdateTick;

    void Update()
    {
        UpdateTicker();
        MilisecondTimeTicker();
        OneSecondTimeTicker();        
    }
    private void UpdateTicker()
    {
        UpdateTick?.Invoke();
    }
    private void MilisecondTimeTicker()
    {
        _msSecond += Time.deltaTime;
        if (_msSecond >= TICK_MAX)
        {
            MilisecondTick?.Invoke(_msSecond);
            _msSecond = 0;            
        }
    }
    private void OneSecondTimeTicker()
    {
        _second += Time.deltaTime;
        if (_second >= 1f)
        {
            OneSecondTick?.Invoke(1f);
            _second = 0;            
        }
        
    }
}
