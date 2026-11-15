using System;
using UnityEngine;
using Game.Scripts.Core;
using Game.Scripts.Gameplay.Views;

namespace Game.Scripts.Gameplay
{
    public class CharacterAxeUse : IDisposable
    {
        private readonly IInputService _input;
        private readonly Transform _cameraTransform;
        
        private readonly float _chopDistance = 2.5f;
        private readonly int _axeDamage = 25;

        // Теперь Zenject сам подставит сюда созданный CharacterView
        public CharacterAxeUse(IInputService input, CharacterView characterView)
        {
            _input = input;
            _cameraTransform = characterView.CameraTransform;

            _input.UseToolPressed += OnUseToolPressed;
        }

        private void OnUseToolPressed()
        {
            Debug.Log("1. Кнопка удара нажата!");

            if (_cameraTransform == null)
            {
                Debug.LogError("ОШИБКА: CameraTransform не назначен в CharacterView!");
                return;
            }

            // Рисуем красный луч в окне Scene на 2 секунды, чтобы видеть, куда он летит
            Debug.DrawRay(_cameraTransform.position, _cameraTransform.forward * _chopDistance, Color.red, 2f);

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hit, _chopDistance))
            {
                Debug.Log($"2. Луч попал в объект: {hit.collider.gameObject.name}");

                // Ищем скрипт Tree на самом объекте ИЛИ на его родителе (частая проблема префабов)
                Tree tree = hit.collider.GetComponentInParent<Tree>();
        
                if (tree != null)
                {
                    Debug.Log("3. Нашли дерево! Наносим урон.");
                    tree.TakeDamage(_axeDamage);
                }
                else
                {
                    Debug.LogWarning("Луч попал в объект, но на нем и его родителях нет скрипта Tree.");
                }
            }
            else
            {
                Debug.Log("2. Луч ни во что не попал (слишком далеко или нет коллайдера).");
            }
        }


        public void Dispose()
        {
            _input.UseToolPressed -= OnUseToolPressed;
        }
    }
}