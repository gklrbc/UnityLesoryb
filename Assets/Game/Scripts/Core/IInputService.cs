using System;
using UnityEngine;

namespace Game.Scripts.Core
{
    public interface IInputService
    {
        Vector2 Direction { get; }
        event Action JumpPressed;
        event Action UseToolPressed;
        public event Action<Vector2> LookPerformed;
    }
}