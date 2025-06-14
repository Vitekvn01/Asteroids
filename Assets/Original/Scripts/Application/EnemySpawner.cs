using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using Original.Scripts.Core.Enemy;
using Original.Scripts.Core.Interfaces;

public class EnemySpawner
{
    private const int InitialSpawnCount = 20;
    private const int SpawnIntervalSeconds = 5;
    private const int MaxAsteroids = 40;

    private IEnemyPool _enemyPool;

    private List<IEnemy> _spawnedEnemies = new();

    [Inject]
    public EnemySpawner(IEnemyPool enemyPool)
    {
        _enemyPool = enemyPool;
        Start();
    }

    public void Start()
    {
        SpawnInitial();
        LoopSpawning().Forget();
    }

    private void SpawnInitial()
    {
        for (int i = 0; i < InitialSpawnCount; i++)
        {
            SpawnAsteroidAtRandomPosition();
        }
    }

    private async UniTaskVoid LoopSpawning()
    {
        while (true)
        {
            await UniTask.Delay(SpawnIntervalSeconds * 1000);

            int currentAsteroids = _spawnedEnemies.Count(e => e.IsActive);
            int canSpawn = Mathf.Min(InitialSpawnCount, MaxAsteroids - currentAsteroids);

            for (int i = 0; i < canSpawn; i++)
            {
                SpawnAsteroidAtRandomPosition();
            }
        }
    }

    private void SpawnAsteroidAtRandomPosition()
    {
        Vector2 randomPos = GetRandomSpawnPosition();
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        IEnemy asteroid = _enemyPool.Get(EnemyType.Asteroid, randomPos, randomRotation);
        
        _spawnedEnemies.Add(asteroid);

        asteroid.OnEnemyDeath += OnAsteroidDeath;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(-100f, 100f);
        float y = Random.Range(-50f, 50f);
        return new Vector2(x, y);
    }

    private void OnAsteroidDeath(IEnemy enemy)
    {
        _spawnedEnemies.Remove(enemy);
        enemy.OnEnemyDeath -= OnAsteroidDeath;
    }
}
