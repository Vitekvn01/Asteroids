using Original.Scripts.Core.Entity;
using Original.Scripts.Core.Entity.Projectiles;
using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IProjectilePool
    {
        public Projectile AddToPool(ProjectileType type);

        public Projectile Get(ProjectileType type, Vector3 pos, Quaternion rotation);
    }
}