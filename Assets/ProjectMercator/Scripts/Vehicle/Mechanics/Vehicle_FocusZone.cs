using System;
using UnityEngine;
/// <summary>
/// При нажатии клавиши, отвечающаю за Interacting, плавно переводит и поворачивает камеру на нужную точку в мире
/// </summary>
public class Vehicle_FocusZone : MonoBehaviour, IInteractable
{
        
    [SerializeField] private Transform _cameraPoint;
    [SerializeField, Tooltip("Дистанция для взаимодействия")] private float _distanceInteracting;
    [SerializeField] private AnimationCurve _moveSpeedCameraCurve;
    [SerializeField] private float _moveSpeedCameraDuration;

    [SerializeField] private float _moveSpeedCameraTime;
    private float t; // переменная от 0 до 1 для интерполяции, применяется в проверках, чтобы перемещение занимало ровно столько времени, сколько записано в _moveSpeedCameraDuration

    private Quaternion _cameraLookPosition;
    private Camera _playerCamera;
    private Vector3 _prevCameraPosition;
    private Quaternion _prevCameraRotation;

    private bool _isCancelPerformed;
    private bool _isInteractActive;

    public static event Action<bool> IsMovementActive;

    private void Awake()
    {
        //instance = this;
        Ray ray = new(_cameraPoint.position, _cameraPoint.rotation * Vector3.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo, 1f);
        _cameraLookPosition = hitInfo.transform.rotation;
        
        if (_moveSpeedCameraCurve == null || _moveSpeedCameraCurve.keys.Length == 0)
        {
            _moveSpeedCameraCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);            
        }
    }
    public void Interact(GameObject interacting)
    {
        if (Vector3.Distance(interacting.transform.position, _cameraPoint.position) <= _distanceInteracting && _isInteractActive == false)
        {
            _isInteractActive = true;
            _playerCamera = interacting.GetComponentInChildren<Camera>();

            _prevCameraPosition = _playerCamera.transform.position;
            _prevCameraRotation = _playerCamera.transform.rotation;

            _moveSpeedCameraTime = 0f;

            Player_Interact.CancelInteraction += GetCancelPerformed;
            Global_TimeTicker.UpdateTick += MoveCameraToPoint;

            IsMovementActive?.Invoke(false);
        }        
    }
    
    private void MoveCameraToPoint()
    {
        _playerCamera.transform.position += MoveDeltaCount(_playerCamera.transform.position, _cameraPoint.position);
        _playerCamera.transform.rotation = Quaternion.Lerp(_playerCamera.transform.rotation, _cameraLookPosition, t);

        if (t >= 1f)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Global_TimeTicker.UpdateTick -= MoveCameraToPoint;
            Global_TimeTicker.UpdateTick += ReturnCameraToPlayer;
        }
    }

    public void ReturnCameraToPlayer()
    {
        if (_isCancelPerformed)
        {            
            _playerCamera.transform.position += MoveDeltaCount(_playerCamera.transform.position, _prevCameraPosition);
            _playerCamera.transform.rotation = Quaternion.Lerp(_playerCamera.transform.rotation, _prevCameraRotation, t);
            
            if (t >= 1f)
            {
                
                Global_TimeTicker.UpdateTick -= ReturnCameraToPlayer;
                Player_Interact.CancelInteraction -= GetCancelPerformed;

                IsMovementActive?.Invoke(true);

                _isCancelPerformed = false;
                _isInteractActive = false;
            }
        }
    }

    private Vector3 LerpPosition(Vector3 PlayerCameraPosition, Vector3 PointCameraPosition, float curveValue)
    {
        return Vector3.Lerp(PlayerCameraPosition, PointCameraPosition, curveValue);        
    }

    private Vector3 MoveDeltaCount(Vector3 PlayerCameraPosition, Vector3 PointCameraPosition)
    {
        _moveSpeedCameraTime += Time.deltaTime;
        t = Mathf.Clamp01(_moveSpeedCameraTime / _moveSpeedCameraDuration);
        float curveValue = _moveSpeedCameraCurve.Evaluate(t);

        Vector3 desiredPosition = LerpPosition(PlayerCameraPosition, PointCameraPosition, curveValue);
        Vector3 moveDelta = desiredPosition - PlayerCameraPosition;
        return moveDelta;
    }

    private void GetCancelPerformed(bool performed)
    {
        _isCancelPerformed = performed;
        Cursor.lockState = CursorLockMode.Locked;
        _moveSpeedCameraTime = 0f;
        t = 0f;
    }


    // кусок кода позволяющий получить мировые координаты с помощью рэйкаста из нужной точки
    // в этом случае это нужно для того, чтобы правильно соориентировать камеру в пространстве, чтобы смотрела под правильным углом и в нужную точку
#if UNITY_EDITOR
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Ray ray = new(_cameraPoint.position, _cameraPoint.rotation * Vector3.forward);
        Physics.Raycast(ray, out RaycastHit hitInfo, 1f);
        _cameraLookPosition = hitInfo.transform.rotation;
        Gizmos.DrawRay(ray.origin, ray.direction * 1f);
        Gizmos.DrawWireSphere(hitInfo.point, 0.1f);
        Gizmos.DrawWireSphere(hitInfo.point, _distanceInteracting);
    }
#endif
    private void OnDisable()
    {
        Player_Interact.CancelInteraction -= GetCancelPerformed;
        Global_TimeTicker.UpdateTick -= MoveCameraToPoint;
        Global_TimeTicker.UpdateTick -= ReturnCameraToPlayer;
    }
    private void OnDestroy()
    {
        Player_Interact.CancelInteraction -= GetCancelPerformed;
        Global_TimeTicker.UpdateTick -= MoveCameraToPoint;
        Global_TimeTicker.UpdateTick -= ReturnCameraToPlayer;
    }
}