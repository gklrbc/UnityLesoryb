using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "NewTree", menuName = "Game/Tree Data")]
    public class TreeData : ScriptableObject
    {
        [Header("Основные настройки")]
        public string treeName = "Обычное дерево";
        public int maxHealth = 100;

        [Header("Награда и остатки")]
        public GameObject woodPrefab;          // Префаб бревна
        [Min(0)] public int minWoodAmount = 2; // Минимум бревен
        [Min(1)] public int maxWoodAmount = 4; // Максимум бревен
        public GameObject stumpPrefab;
    }
}