using Original.Scripts.Core.Entity.Projectiles;
using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IProjectilePool
    {
        public Projectile Get(ProjectileType type, Vector3 pos, Quaternion rotation);
    }
}