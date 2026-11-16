using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Player_CameraMove : MonoBehaviour
{
    private Player _player;
    private Camera _camera;
    private Vector2 _rotation;
    private float _sensitivity;    
    private bool _canMove = true;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _camera = GetComponentInChildren<Camera>();
        _sensitivity = _player.settings.mouseSensitivity;
    }
    public void RotateCamera(Vector2 mouseDelta)
    {
        if (_canMove)
        {
            mouseDelta *= _sensitivity;
            _rotation.y += mouseDelta.x;
            _rotation.x = Mathf.Clamp(_rotation.x - mouseDelta.y, -90, 90);

            _camera.transform.eulerAngles = _rotation;
        }        
    }

    public void CanMove(bool isCan)
    {
        _canMove = isCan;
    }
}
