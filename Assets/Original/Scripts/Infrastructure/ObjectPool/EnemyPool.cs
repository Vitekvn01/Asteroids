using System;
using System.Collections.Generic;
using System.Linq;
using Original.Scripts.Core.Enemy;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.ObjectPool
{
    public class EnemyPool : IEnemyPool
    {
        private const int InitialSizeAsteroid = 100;
        private const int InitialSizeDebris = 100;
        private const int InitialSizeUfo = 100;
    
        private readonly Transform _parent;
    
        private readonly IEnemyFactory _enemyFactory;
    
        private readonly List<IEnemy> _pool = new();
        
        [Inject]
        public EnemyPool(IEnemyFactory enemyFactory, Transform parent = null) 
        {
            _enemyFactory = enemyFactory;
            _parent = parent;
        
            for (int i = 0; i < InitialSizeAsteroid; i++)
            {
                AddToPool(EnemyType.Asteroid);
            }
            
            for (int i = 0; i < InitialSizeDebris; i++)
            {
                AddToPool(EnemyType.Debris);
            }
            
            for (int i = 0; i < InitialSizeUfo; i++)
            {
                AddToPool(EnemyType.Ufo);
            }

        }

        
        public IEnemy AddToPool(EnemyType type)
        {   
            Vector3 pos = new Vector3(0, 0, 0);
            float angleZ = 0;
            
            IEnemy instance;

            switch (type)
            {
                case EnemyType.Asteroid:
                    instance = _enemyFactory.Create(EnemyType.Asteroid, pos, angleZ, _parent);
                    break;
                case EnemyType.Ufo:
                    instance = _enemyFactory.Create(EnemyType.Ufo, pos, angleZ, _parent);;
                    break;
                case EnemyType.Debris:
                    instance = _enemyFactory.Create(EnemyType.Debris, pos, angleZ, _parent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, $"Unhandled enemy type: {type}");
            }
            
            instance.Deactivate();
            _pool.Add(instance);
            return instance;
        }

        public IEnemy Get(EnemyType enemyType, Vector3 pos, Quaternion rotation)
        {
            var instance = _pool.FirstOrDefault(enemy
                => !enemy.IsActive && MatchType(enemy, enemyType)) ?? AddToPool(enemyType);
            
            instance.Activate(pos, rotation);
            return instance;
        }
        
        private bool MatchType(IEnemy enemy, EnemyType enemyType)
        {
            bool isMatch = false;
            
            switch (enemyType)
            {
                case EnemyType.Asteroid:
                    isMatch = enemy is Asteroid;
                    break;
                case EnemyType.Ufo:
                    isMatch = enemy is Ufo;
                    break;
                case EnemyType.Debris:
                    isMatch = enemy is Debris;
                    break;
                default:
                    throw new ArgumentException("Unknown enemy type", nameof(enemyType));
            };

            return isMatch;
        }
    }
}