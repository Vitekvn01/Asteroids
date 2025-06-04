using UnityEngine;
using Zenject;

public class ProjectileFactory : IProjectileFactory
{
    private readonly TickableManager _tickableManager;
    private readonly ProjectileBehavior _standardProjectilePrefab;
    private readonly ICustomPhysicsFactory _physicsFactory;
    
    [Inject]
    public ProjectileFactory( TickableManager tickableManager, ProjectileBehavior standardProjectilePrefab, ICustomPhysicsFactory physicsFactory)
    {
        _tickableManager = tickableManager;
        _standardProjectilePrefab = standardProjectilePrefab;
        _physicsFactory = physicsFactory;
    }

    public Projectile Create(Vector3 position, Quaternion rotation)
    {
        ProjectileBehavior createdView =
            Object.Instantiate(_standardProjectilePrefab, position, rotation);

        CustomPhysics physics = _physicsFactory.Create(createdView.transform.position, createdView.transform.rotation.eulerAngles.z);

        Projectile created = new Projectile(createdView, physics);
        _tickableManager.Add(created);
        return created;
    }
}