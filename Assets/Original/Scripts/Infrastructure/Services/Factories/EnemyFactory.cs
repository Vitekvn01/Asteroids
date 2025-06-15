using System;
using Original.Scripts.Core.Enemy;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private const float MinSpeed = 1f;
        private const float MaxSpeed = 5;
        
        private readonly DiContainer _diContainer;
        private readonly TickableManager _tickableManager;
        private readonly AsteroidBehaviour _asteroidPrefab;
        private readonly ICustomPhysicsFactory _physicsFactory;
    
        [Inject]
        public EnemyFactory(DiContainer diContainer, TickableManager tickableManager, 
            AsteroidBehaviour asteroidPrefab, ICustomPhysicsFactory physicsFactory)
        {
            _diContainer = diContainer;
            _tickableManager = tickableManager;
            _asteroidPrefab = asteroidPrefab;
            _physicsFactory = physicsFactory;
        }

        public IEnemy Create(EnemyType enemyType, Vector3 position, float rotation = 0, Transform parent = null)
        {
            float speed = Random.Range(MinSpeed, MaxSpeed);
            
            switch (enemyType)
            {
                case EnemyType.Asteroid:
                    return CreateAsteroid(position, rotation, speed, parent);
                case EnemyType.Ufo:
                    throw new ArgumentException("Unknown weapon type", nameof(enemyType));
                default:
                    throw new ArgumentException("Unknown weapon type", nameof(enemyType));
            }
            
        }

        private Asteroid CreateAsteroid(Vector3 position, float rotation, float speed, Transform parent)
        {
            AsteroidBehaviour createdView =
                _diContainer.InstantiatePrefabForComponent<AsteroidBehaviour>(_asteroidPrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = false;
            bool isActive = true;
            PhysicsLayer asteroidLayer = PhysicsLayer.Enemy;
            PhysicsLayer collisionMask = PhysicsLayer.Player;
            
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger, isActive, asteroidLayer, collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 1, customCollider);
        
            Asteroid created = new Asteroid(createdView, physics, speed);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }
    }
}