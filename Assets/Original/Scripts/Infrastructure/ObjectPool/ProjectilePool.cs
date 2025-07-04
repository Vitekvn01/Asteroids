using System.Collections.Generic;
using System.Linq;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.ObjectPool
{
    public class ProjectilePool : IProjectilePool
    {
        private const int InitialSize = 100;

        private readonly Transform _parent;
        private readonly IProjectileFactory _projectileFactory;

        private readonly List<Projectile> _pool = new();

        [Inject]
        public ProjectilePool(IProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
            
            var parentObj = new GameObject("ProjectilePool");
            _parent = parentObj.transform;
        
            for (int i = 0; i < InitialSize; i++)
            {
                AddToPool(ProjectileType.Bullet);
                AddToPool(ProjectileType.EnemyBullet);
                AddToPool(ProjectileType.Laser);
            }
        }
        
        public Projectile Get(ProjectileType type, Vector3 pos, Quaternion rotation)
        {
            var instance = _pool.FirstOrDefault
                (p => !p.IsActive && MatchType(p, type)) ?? AddToPool(type);
            instance.Activate(pos, rotation);
            return instance;
        }
        
        private Projectile AddToPool(ProjectileType type)
        {
            Vector3 pos = Vector3.zero;
            float angleZ = 0;
        
            Projectile instance = _projectileFactory.Create(type, pos, angleZ, _parent);
            instance.Deactivate();

            _pool.Add(instance);
            return instance;
        }
    
        private bool MatchType(Projectile projectile, ProjectileType type)
        {
            return projectile.Type == type;
        }
    }
}