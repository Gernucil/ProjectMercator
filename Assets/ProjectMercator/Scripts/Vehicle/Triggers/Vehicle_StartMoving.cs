using UnityEngine;
using UnityEngine.VFX;

public class Vehicle_StartMoving : MonoBehaviour, IInteractable
{
    [Header("Управление текстурой снега на земле")]
    [SerializeField] private Material _groundTexture;
    [SerializeField] private float _speedLimit;
    [SerializeField, Min(0.1f)] private float _gainSpeed;
    [Header("Управление частицами снега")]
    [SerializeField] private VisualEffect _snowFall;
    [SerializeField] private float _directionLimit;    
    [SerializeField] private float _particleSpeedLimit;
    [SerializeField, Min(0.1f)] private float _directionGainSpeed;
    [SerializeField, Min(0.1f)] private float _particleGainSpeed;

    private Vector3 _particleDirection;
    private Vector3 _changeDirection;
    private Vector2 _scrollSpeed;
    private Vector2 _currentOffset;

    private float _particleChangeSpeed;
    private float _particleCurrentSpeed;

    private bool _startMove = false;

    public void Interact(GameObject interacting)
    {
        _startMove = true;
    }

    void Update()
    {
        if (_startMove)
        {
            StartMoveVehicle();
            ChangeParticleDirection();
        }
    }

    private void StartMoveVehicle()
    {
        if (_scrollSpeed.x < _speedLimit) _scrollSpeed.x += _gainSpeed;
        
        _currentOffset.x += _scrollSpeed.x * Time.deltaTime;

        _groundTexture.SetVector("_Offset", _currentOffset);
    }
    private void ChangeParticleDirection()
    {
        if (_changeDirection.x < _directionLimit) _changeDirection.x += _directionGainSpeed;
        if (_particleChangeSpeed < _particleSpeedLimit) _particleChangeSpeed += _particleGainSpeed;

        if (_particleDirection.x < _directionLimit) _particleDirection.x += _changeDirection.x * Time.deltaTime;
        if (_particleCurrentSpeed < _particleSpeedLimit) _particleCurrentSpeed += _particleChangeSpeed * Time.deltaTime;
        _particleDirection.y = -1;
        _particleDirection.z = 0;
        _snowFall.SetVector3("ParticleDirection", _particleDirection);        
        _snowFall.SetFloat("ParticleSpeed", _particleCurrentSpeed);
    }
}