using UnityEngine;

public class EnemyBuilder : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    public Enemy Create()
    {
       return Instantiate(_enemyPrefab);
    }

    public void Create(Vector3 position)
    {
        Instantiate(_enemyPrefab, position, Quaternion.identity);
    }
}
