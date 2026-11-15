using UnityEngine;
namespace Game.Scripts.Gameplay
{
    public class Tree : MonoBehaviour
    {
        [Header("Настройки типа")]
        [SerializeField] private TreeData _treeData;
        
        private int currentHealth;

        private void Start()
        {
            if (_treeData != null)
            {
                currentHealth = _treeData.maxHealth;
            }
            else
            {
                Debug.LogError("Не назначен TreeData на дереве: " + gameObject.name);
            }
        }

        // Теперь метод принимает только урон
        public void TakeDamage(int damage)
        {
            if (_treeData == null) return;

            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                ChopDown();
            }
        }

        private void ChopDown()
        {
            // 1. Спавним пень
            if (_treeData.stumpPrefab != null)
            {
                Instantiate(_treeData.stumpPrefab, transform.position, transform.rotation);
            }

            // 2. Спавним случайное количество бревен
            if (_treeData.woodPrefab != null)
            {
                int min = Mathf.Min(_treeData.minWoodAmount, _treeData.maxWoodAmount);
                int max = Mathf.Max(_treeData.minWoodAmount, _treeData.maxWoodAmount);
                int randomWoodCount = Random.Range(min, max + 1);

                for (int i = 0; i < randomWoodCount; i++)
                {
                    Vector3 spawnPos = transform.position + new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f));
                    Instantiate(_treeData.woodPrefab, spawnPos, Quaternion.identity);
                }
            }

            // 3. Удаляем само дерево
            Destroy(gameObject);
        }
    }
}