using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Original.Scripts.Core;
using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Application.Gameplay.Spawner
{
    public class EnemySpawner
    {
        private readonly int _initialSpawnCount;
        private readonly int _spawnIntervalSeconds;
        private readonly int _maxEnemies ;
        private readonly float _ufoSpawnChance;
    
        private readonly PlayerSpawner _playerSpawner;
        private readonly IEnemyPool _enemyPool;
        private readonly WorldBounds _worldBounds;
        private readonly List<IEnemy> _spawnedEnemies = new();
    
        private bool _isSpawning;

        [Inject]
        public EnemySpawner(IEnemyPool enemyPool, PlayerSpawner playerSpawner, WorldBounds worldBounds, IConfigProvider configProvider)
        {
            _enemyPool = enemyPool;
            _playerSpawner = playerSpawner;
            _worldBounds = worldBounds;

            _initialSpawnCount = configProvider.WorldConfig.InitialSpawnCount;
            _spawnIntervalSeconds = configProvider.WorldConfig.SpawnIntervalSeconds;
            _maxEnemies = configProvider.WorldConfig.MaxEnemies;
            _ufoSpawnChance = configProvider.WorldConfig.UfoSpawnChance;
        }

        public void Start()
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                SpawnInitial();
                LoopSpawning().Forget();
            }
        }
    
        public void Stop()
        {
            _isSpawning = false;
        }

        private void SpawnInitial()
        {
            for (int i = 0; i < _initialSpawnCount; i++)
            {
                SpawnAsteroidAtEdge();
            }
        }

        private async UniTaskVoid LoopSpawning()
        {
            while (_isSpawning)
            {
                int currentCount = _spawnedEnemies.Count(e => e.IsActive);

                if (currentCount < _maxEnemies)
                {
                    if (Random.value < _ufoSpawnChance)
                    {
                        SpawnUfoAtEdge();
                    }
                    else
                    {
                        SpawnAsteroidAtEdge();
                    }
        
                }

                await UniTask.Delay(_spawnIntervalSeconds * 1000);
            }
        }



        private void SpawnAsteroidAtEdge()
        {
            var (pos, dir) = GetEdgeSpawn();
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));

            IEnemy asteroid = _enemyPool.Get(EnemyType.Asteroid, pos, rot);
            _spawnedEnemies.Add(asteroid);
            asteroid.OnEnemyDeath += OnAsteroidDeath;
        }

        private void SpawnUfoAtEdge()
        {
            var (pos, dir) = GetEdgeSpawn();
            Quaternion rot = Quaternion.identity;

            IEnemy ufo = _enemyPool.Get(EnemyType.Ufo, pos, rot);
            ((Ufo)ufo).SetTarget(_playerSpawner.ShipController.Ship);
            _spawnedEnemies.Add(ufo);
            ufo.OnEnemyDeath += OnUFODeath;
        }

        private (Vector2 pos, Vector2 dir) GetEdgeSpawn()
        {
            float halfWidth = _worldBounds.Width / 2f;
            float halfHeight = _worldBounds.Height / 2f;
            int side = Random.Range(0, 4);

            Vector2 pos;
            Vector2 dir;
            switch (side)
            {
                case 0:
                    pos = new Vector2(-halfWidth - 1, Random.Range(-halfHeight, halfHeight)); dir = Vector2.right; break;
                case 1:
                    pos = new Vector2(halfWidth + 1, Random.Range(-halfHeight, halfHeight)); dir = Vector2.left; break;
                case 2:
                    pos = new Vector2(Random.Range(-halfWidth, halfWidth), halfHeight + 1); dir = Vector2.down; break;
                default:
                    pos = new Vector2(Random.Range(-halfWidth, halfWidth), -halfHeight - 1); dir = Vector2.up; break;
            }
            dir = (dir + Random.insideUnitCircle * 0.3f).normalized;
            return (pos, dir);
        }

        private void OnAsteroidDeath(IEnemy enemy)
        {
            SpawnDebris(enemy.Physics);
            _spawnedEnemies.Remove(enemy);
            enemy.OnEnemyDeath -= OnAsteroidDeath;
        }

        private void OnUFODeath(IEnemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
            enemy.OnEnemyDeath -= OnUFODeath;
        }

        private void OnDebrisDeath(IEnemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
            enemy.OnEnemyDeath -= OnDebrisDeath;
        }

        private void SpawnDebris(CustomPhysics physics)
        {
            int count = Random.Range(3, 6);
            for (int i = 0; i < count; i++)
            {
                float angle = Random.Range(0f, 360f);
                Quaternion rot = Quaternion.Euler(0, 0, angle);

                IEnemy debris = _enemyPool.Get(EnemyType.Debris, physics.Position, rot);
                ((Debris)debris).SetSpeed(physics.Velocity.magnitude);

                _spawnedEnemies.Add(debris);
                debris.OnEnemyDeath += OnDebrisDeath;
            }
        }
    }
}
