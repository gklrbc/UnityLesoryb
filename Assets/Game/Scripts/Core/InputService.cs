using System;
using Game.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputService : IInputService, IInitializable, IDisposable
{
    private InputActions _input = new();

    private Vector2 _direction;
    private bool _isUsing;

    public event Action JumpPressed;
    public event Action<Vector2> LookPerformed;
    public event Action UseToolPressed;

    public Vector2 Direction => _direction;
    public bool IsUsing => _isUsing;

    public void Initialize()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        _input.Enable();

        _input.Player.Walking.started += OnMove;
        _input.Player.Walking.performed += OnMove;
        _input.Player.Walking.canceled += OnMove;
        _input.Player.Jump.started += OnJump;
        _input.Player.UseTool.started += OnUse;
        _input.Player.UseTool.canceled += OnUse;
        _input.Player.Look.performed += OnLook;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpPressed?.Invoke();
    }

    private void OnUse(InputAction.CallbackContext context)
    {
        _isUsing = context.ReadValueAsButton();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        LookPerformed?.Invoke(context.ReadValue<Vector2>());
    }

    private void UnsubscribeFromEvents()
    {
        _input.Player.Walking.started -= OnMove;
        _input.Player.Walking.performed -= OnMove;
        _input.Player.Walking.canceled -= OnMove;
        _input.Player.Jump.started -= OnJump;
        _input.Player.UseTool.started -= OnUse;
        _input.Player.UseTool.canceled -= OnUse;
        _input.Player.Look.performed -= OnLook;

        _input.Disable();
    }

    public void Dispose()
    {
        UnsubscribeFromEvents();
    }
}