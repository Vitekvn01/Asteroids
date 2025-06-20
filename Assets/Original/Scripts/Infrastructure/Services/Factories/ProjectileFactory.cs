using System;
using Original.Scripts.Core;
using Original.Scripts.Core.Entity;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly DiContainer _diContainer;
        private readonly TickableManager _tickableManager;
        private readonly ProjectileBehaviour _standardProjectilePrefab;
        private readonly ICustomPhysicsFactory _physicsFactory;
    
        [Inject]
        public ProjectileFactory(DiContainer diContainer, TickableManager tickableManager, 
            ProjectileBehaviour standardProjectilePrefab, ICustomPhysicsFactory physicsFactory)
        {
            _diContainer = diContainer;
            _tickableManager = tickableManager;
            _standardProjectilePrefab = standardProjectilePrefab;
            _physicsFactory = physicsFactory;
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
                    throw new ArgumentException("dont created void type", nameof(projectileType));
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
                _diContainer.InstantiatePrefabForComponent<ProjectileBehaviour>(_standardProjectilePrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = true;
            bool isActive = true;
            PhysicsLayer layer = PhysicsLayer.Projectile;
            PhysicsLayer collisionMask = PhysicsLayer.Enemy | PhysicsLayer.Player;
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger, isActive, layer, collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 0, customCollider);
        
            Projectile created = new Projectile(createdView, physics, 50, ProjectileType.Bullet);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }
        
        public Projectile CreateEnemyBullet(Vector3 position, float rotation = 0, Transform parent = null)
        {
            ProjectileBehaviour createdView =
                _diContainer.InstantiatePrefabForComponent<ProjectileBehaviour>(_standardProjectilePrefab.gameObject,
                    position, Quaternion.Euler(0, 0, rotation), parent);

            bool isTrigger = true;
            bool isActive = true;
            PhysicsLayer layer = PhysicsLayer.Projectile;
            PhysicsLayer collisionMask = PhysicsLayer.Player;
            ICustomCollider customCollider = new CustomCollider(createdView.RadiusCollider, isTrigger, isActive, layer, collisionMask);
        
            CustomPhysics physics = _physicsFactory.Create(createdView.transform.position,
                createdView.transform.rotation.eulerAngles.z, 0, 0, customCollider);
        
            Projectile created = new Projectile(createdView, physics, 50, ProjectileType.EnemyBullet);
        
            customCollider.SetHandler(created);
        
            _tickableManager.Add(created);
            return created;
        }
    
    
    }
    
}