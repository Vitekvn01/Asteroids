using Original.Scripts.Core.Entity;
using Original.Scripts.Core.Entity.Projectiles;
using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IProjectileFactory
    {
        public Projectile Create(ProjectileType projectileType, Vector3 position, float rotation = 0,
            Transform parent = null);
    }
    
}