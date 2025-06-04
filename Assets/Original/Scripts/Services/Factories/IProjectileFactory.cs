using UnityEngine;

public interface IProjectileFactory
{
    public Projectile Create(Vector3 position, Quaternion rotation);
}