using System;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Interfaces.IPhysics;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using Original.Scripts.Presentation.Behavior;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly DiContainer _diContainer;
        private readonly TickableManager _tickableManager;
  
        
        private readonly ProjectileBehaviour _bulletProjectilePrefab;
        private readonly LaserProjectileBehavior _laserProjectilePrefab;
        private readonly ICustomPhysicsFactory _physicsFactory;
        
        private readonly IConfigProvider _configProvider;
    
        [Inject]
        public ProjectileFactory(DiContainer diContainer, TickableManager tickableManager, 
            ProjectileBehaviour bulletProjectilePrefab, LaserProjectileBehavior laserProjectilePrefab,
            ICustomPhysicsFactory physicsFactory, IConfigProvider configProvider)
        {
            _diContainer = diContainer;
            _tickableManager = tickableManager;
            _bulletProjectilePrefab = bulletProjectilePrefab;
            _laserProjectilePrefab = laserProjectilePrefab;
            _physicsFactory = physicsFactory;
            _configProvider = configProvider;
        }

        public Projectile Create(ProjectileType projectileType, Vector3 position, float rotation = 0,
            Transform parent = null)
        {
            Projectile created;
        
            switch (projectileType)
            {
                case ProjectileType.Bullet:
                    created = CreateBullet(position, rotation, parent);
                    break;
                case ProjectileType.Laser:
                    created = CreateLaser(position, rotation, parent);
                    break;
                case ProjectileType.EnemyBullet:
                    created = CreateEnemyBullet(position, rotation, parent);
                    break;
                default:
                    throw new ArgumentException("Unknown ProjectileType type", nameof(projectileType));
            }
        
            return created;
        }
        
        public Projectile CreateBullet(Vector3 position, float rotation = 0, Transform parent = null)
        {
            ProjectileBehaviour createdView =
                _diContainer.InstantiatePrefabForComponent<ProjectileBehaviour>(_bulletProjectilePrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = true;
            PhysicsLayer layer = PhysicsLayer.Projectile;
            PhysicsLayer collisionMask = PhysicsLayer.Enemy | PhysicsLayer.Player;
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger, layer,
                collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 0, customCollider);
        
            Projectile created = new Projectile(createdView, physics,
                _configProvider.WeaponConfig.BulletProjectileSpeed, _configProvider.WeaponConfig.BulletLifetime,
                ProjectileType.Bullet);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }
        
        public Projectile CreateLaser(Vector3 position, float rotation = 0, Transform parent = null)
        {
            LaserProjectileBehavior createdView =
                _diContainer.InstantiatePrefabForComponent<LaserProjectileBehavior>(_laserProjectilePrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = true;
            PhysicsLayer layer = PhysicsLayer.Projectile;
            PhysicsLayer collisionMask = PhysicsLayer.Enemy;
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger, layer,
                collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 0, customCollider);
        
            Projectile created = new LaserProjectile(createdView, physics,
                _configProvider.WeaponConfig.LaserProjectileSpeed,
                _configProvider.WeaponConfig.LaserLifetime, ProjectileType.Laser);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }
        
        public Projectile CreateEnemyBullet(Vector3 position, float rotation = 0, Transform parent = null)
        {
            ProjectileBehaviour createdView =
                _diContainer.InstantiatePrefabForComponent<ProjectileBehaviour>(_bulletProjectilePrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = true;
            PhysicsLayer layer = PhysicsLayer.Projectile;
            PhysicsLayer collisionMask = PhysicsLayer.Player;
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger, layer, collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 0, customCollider);
        
            Projectile created = new Projectile(createdView, physics, _configProvider.WeaponConfig.EnemyProjectileSpeed,
                _configProvider.WeaponConfig.EnemyBulletLifetime,  ProjectileType.EnemyBullet);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }
    
    
    }
    
}