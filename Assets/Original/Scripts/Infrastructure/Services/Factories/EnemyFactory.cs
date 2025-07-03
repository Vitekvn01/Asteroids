using System;
using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Entity.Enemy.Ufo;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Interfaces.IPhysics;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using Original.Scripts.Presentation.Behavior;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly TickableManager _tickableManager;
        private readonly SignalBus _signalBus;
        
        private readonly AsteroidBehaviour _asteroidPrefab;
        private readonly AsteroidBehaviour _debrisPrefab;
        private readonly UfoBehaviour _ufoBehaviour;
        
        private readonly ICustomPhysicsFactory _physicsFactory;
        private readonly IWeaponFactory _weaponFactory;

        private readonly IConfigProvider _configLoader;
    
        [Inject]
        public EnemyFactory(DiContainer diContainer, TickableManager tickableManager, SignalBus signalBus, 
            AsteroidBehaviour asteroidPrefab, DebrisBehaviour debrisPrefab, ICustomPhysicsFactory physicsFactory,
            UfoBehaviour ufoBehaviour, IWeaponFactory weaponFactory, IConfigProvider configLoader)
        {
            _diContainer = diContainer;
            _tickableManager = tickableManager;
            _signalBus = signalBus;
            
            _asteroidPrefab = asteroidPrefab;
            _debrisPrefab = debrisPrefab;
            _ufoBehaviour = ufoBehaviour;
            
            _physicsFactory = physicsFactory;
            _weaponFactory = weaponFactory;
            
            _configLoader = configLoader;

        }

        public IEnemy Create(EnemyType enemyType, Vector3 position, float rotation = 0, Transform parent = null)
        {
            switch (enemyType)
            {
                case EnemyType.Asteroid:
                    return CreateAsteroid(position, rotation, parent);
                case EnemyType.Ufo:
                    return CreateUfo(position, rotation, parent);
                case EnemyType.Debris:
                    return CreateDebris(position, rotation, parent);
                default:
                    throw new ArgumentException("Unknown weapon type", nameof(enemyType));
            }
            
        }

        private Asteroid CreateAsteroid(Vector3 position, float rotation, Transform parent)
        {
            AsteroidBehaviour createdView =
                _diContainer.InstantiatePrefabForComponent<AsteroidBehaviour>(_asteroidPrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = false;
            PhysicsLayer asteroidLayer = PhysicsLayer.Enemy;
            PhysicsLayer collisionMask = PhysicsLayer.Player;
            
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger,
                asteroidLayer, collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 1, customCollider);
            
            float speed = Random.Range(_configLoader.AsteroidConfig.MinSpeed, _configLoader.AsteroidConfig.MaxSpeed);
            
            Asteroid created = new Asteroid(createdView, _signalBus, physics, speed, EnemyType.Asteroid);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }

        private Ufo CreateUfo(Vector3 position, float rotation, Transform parent)
        {
            UfoBehaviour createdView =
                _diContainer.InstantiatePrefabForComponent<UfoBehaviour>(_ufoBehaviour.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = false;
            PhysicsLayer asteroidLayer = PhysicsLayer.Ufo | PhysicsLayer.Enemy;
            PhysicsLayer collisionMask = PhysicsLayer.Player | PhysicsLayer.Ufo;
            
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger,
                asteroidLayer, collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 2, 1, customCollider);
            
            IWeapon weapon = _weaponFactory.Create(WeaponType.EnemyWeapon);
            
            float speed = Random.Range(_configLoader.UfoConfig.MinSpeed, _configLoader.UfoConfig.MaxSpeed);
            float fireRadius =
                Random.Range(_configLoader.UfoConfig.MinFireRadius, _configLoader.UfoConfig.MaxFireRadius);
            float fireSpread =
                Random.Range(_configLoader.UfoConfig.MinFireSpreadAngle, _configLoader.UfoConfig.MaxFireSpreadAngle);
            
            Ufo created = new Ufo(createdView, _signalBus, physics, weapon, speed, fireRadius,
                fireSpread, EnemyType.Ufo);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }
        
        private Debris CreateDebris(Vector3 position, float rotation, Transform parent)
        {
            DebrisBehaviour createdView =
                _diContainer.InstantiatePrefabForComponent<DebrisBehaviour>(_debrisPrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = false;
            PhysicsLayer asteroidLayer = PhysicsLayer.Enemy;
            PhysicsLayer collisionMask = PhysicsLayer.Player;
            
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger,
                asteroidLayer, collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 1, customCollider);
            
            float speed = 0;
            
            Debris created = new Debris(createdView, _signalBus, physics, speed, EnemyType.Debris);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }

    }
}