using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyBuilder _enemyBuilder;
    [SerializeField] private int _creationTime = 2;

    private List<Vector3> _positions;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = CreatePool();
        _positions = GetPointsPositions();
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), _creationTime, _creationTime);
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.Initialize(ReleaseEnemy,GetTargetPosition());
        enemy.transform.position = GetRandomPosition();
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 target = Random.insideUnitSphere * 100;
        return target = new Vector3(target.x, 1, target.z);
    }

    private List<Vector3> GetPointsPositions()
    {
        List<Vector3> positions = new();
        List<Point> points = new(GetComponentsInChildren<Point>().ToList());

        foreach (Point point in points)
            positions.Add(point.transform.position);

        return positions;
    }

    private ObjectPool<Enemy> CreatePool()
    {
        return new ObjectPool<Enemy>(
            CreateEnemy,
            OnGetEnemy,
            enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); });
    }

    private Vector3 GetRandomPosition()
    {
        return _positions[Random.Range(0, _positions.Count)];
    }

    private Enemy CreateEnemy()
    {
        return _enemyBuilder.Create();
    }

    private void SpawnEnemy() =>
        _pool.Get();

    private void ReleaseEnemy(Enemy enemy) =>
        _pool.Release(enemy);
}
