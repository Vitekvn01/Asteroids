using UnityEngine;

public interface IProjectileFactory
{
    public Projectile Create(Vector3 position, float rotation = 0, Transform parent = null);
}