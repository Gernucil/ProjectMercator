using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Player_Move : MonoBehaviour
{
    private Player _player;
    private CharacterController _characterController;
    private Camera _camera;
    private Vector3 _velocity;
    private float _speed;
    private bool _canMove = true;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();
        _player = GetComponent<Player>();
        _speed = _player.settings.moveSpeed;
    }

    public void Move(Vector2 direction)
    {
        if (_canMove)
        {
            var euler = _camera.transform.eulerAngles;
            _velocity = _speed * Time.deltaTime * direction;
            Vector3 move = Quaternion.Euler(0, euler.y, 0) * new Vector3(_velocity.x, 0, _velocity.y);


            _characterController.Move(move);
        }
    }
    public void CanMove(bool isCan)
    {
        _canMove = isCan;
    }
}
