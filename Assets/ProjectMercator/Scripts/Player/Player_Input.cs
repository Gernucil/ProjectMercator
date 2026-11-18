using System;
using UnityEngine;
using UnityEngine.Windows;

public class Player_Input : MonoBehaviour
{
    private Player_InputManager input;
    private Player_Move move;
    private Player_CameraMove cameraMove;
    private Player_Interact interact;
    private Player_Inventory inventory;

    private Vector2 _mouseDelta;
    private Vector2 _moveInput;
    private bool _isMouseInteracting;

    private void Awake()
    {
        input = new();
        move = GetComponent<Player_Move>();
        cameraMove = GetComponent<Player_CameraMove>();
        interact = GetComponent<Player_Interact>();
        inventory = GetComponent<Player_Inventory>();
        Subscribe();
    }

    private void OnEnable()
    {
        input.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        //EventBus.Subscribe();
        Vehicle_FocusZone.IsMovementActive += InputPlayerActive;
    }

    public void InputPlayerActive(bool isActive)
    {
        if (isActive) IsPlayerCanMove(isActive);
        if (!isActive) IsPlayerCanMove(isActive);
    }
    
    private void IsPlayerCanMove(bool isCan)
    {
        move.CanMove(isCan);
        cameraMove.CanMove(isCan);
    }

    private void Subscribe()
    {
        // события на передвижение
        input.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>(); 
        input.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        // события на перемещение мышки
        input.Player.Look.performed += ctx => cameraMove.RotateCamera(ctx.ReadValue<Vector2>()); 
        input.Player.Look.canceled += ctx => cameraMove.RotateCamera(Vector2.zero);
        // событие на взаимодействие
        input.Player.Interact.performed += ctx => interact.PerformInteract();
        input.Player.MouseInteraction.started += ctx => _isMouseInteracting = true;
        input.Player.MouseInteraction.canceled += ctx => _isMouseInteracting = false;
        input.Player.MousePosition.performed += ctx => _mouseDelta = ctx.ReadValue<Vector2>();
        
        // событие на отмену взаимодействия
        input.Player.Return.performed += ctx => interact.CancelInteract(ctx.performed);
        // событие на дроп предмета
        input.Player.DropItem.performed += ctx => inventory.DropItem();
    }

    private void Unsubscribe()
    {
        input.Dispose();
        Vehicle_FocusZone.IsMovementActive -= InputPlayerActive;
    }

    private void LateUpdate()
    {
        if (_moveInput.sqrMagnitude > 0.01f) move.Move(_moveInput);
        if (_isMouseInteracting && Cursor.lockState != CursorLockMode.Locked)
        {
            interact.MouseMoveDelta(_mouseDelta);
            interact.MousePerformInteract();            
        }
        
    }

    private void OnDisable()
    {
        input.Player.Disable();
        Cursor.lockState = CursorLockMode.Confined;
        Unsubscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
