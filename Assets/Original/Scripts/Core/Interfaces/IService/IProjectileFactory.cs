using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IProjectileFactory
    {
        public Projectile Create(Vector3 position, float rotation = 0, Transform parent = null);
    }
}