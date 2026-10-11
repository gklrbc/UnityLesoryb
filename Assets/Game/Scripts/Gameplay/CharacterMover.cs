using Game.Scripts.Core;
using Game.Scripts.Gameplay.Views;
using UnityEngine;
using Zenject;

public class CharacterMover : ITickable
{
    private IInputService _input;
    private CharacterView _characterView;

    private float _maxSpeed;
    private float _jumpSpeed;
    private float _sensitivity;

    private Vector3 _velocity;
    private float _gravity = 9.81f;

    public CharacterMover(IInputService input, CharacterView characterView)
    {
        _input = input;
        _characterView = characterView;

        _maxSpeed = _characterView.MaxSpeed;
        _jumpSpeed = _characterView.JumpSpeed;
        _sensitivity = _characterView.Sensitivity;

        _input.JumpPressed += Jump;
        _input.LookPerformed += Rotate;
    }

    private void Jump()
    {
        if (_characterView.CharacterController.isGrounded == false)
            return;

        _velocity.y = _jumpSpeed;
    }

    private void Move()
    {
        _characterView.CharacterController.Move(_velocity * Time.deltaTime);
    }

    private void Rotate(Vector2 lookDelta)
    {
        _characterView.CharacterController.transform.Rotate(0f, lookDelta.x * _sensitivity, 0f);
    }

    private void ChangeVelocity()
    {
        if (_characterView.CharacterController.isGrounded == false)
            _velocity.y -= _gravity * Time.deltaTime;

        Vector3 direction = new Vector3(_input.Direction.x, 0f, _input.Direction.y);
        direction = _characterView.CharacterController.transform.TransformDirection(direction);

        _velocity.x = direction.x * _maxSpeed;
        _velocity.z = direction.z * _maxSpeed;
    }

    public void Tick()
    {
        ChangeVelocity();
        Move();
    }
}