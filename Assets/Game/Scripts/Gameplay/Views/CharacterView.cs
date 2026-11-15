using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Game.Scripts.Gameplay.Views
{
    public class CharacterView : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraTransform { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Transform Head { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; } = 5.0f;
        [field: SerializeField] public float JumpSpeed { get;private set; } = 5.0f;
        [field: SerializeField] public float Sensitivity { get;private set; } = 3.0f;
        [field: SerializeField] public float MaxVerticalAngle { get;private set; } = 60.0f;
    }
}