using UnityEngine;
using Zenject;

public class ProjectileFactory : IProjectileFactory
{
    private readonly DiContainer _diContainer;
    private readonly TickableManager _tickableManager;
    private readonly ProjectileBehavior _standardProjectilePrefab;
    private readonly ICustomPhysicsFactory _physicsFactory;
    
    [Inject]
    public ProjectileFactory(DiContainer diContainer, TickableManager tickableManager, ProjectileBehavior standardProjectilePrefab, ICustomPhysicsFactory physicsFactory)
    {
        _diContainer = diContainer;
        _tickableManager = tickableManager;
        _standardProjectilePrefab = standardProjectilePrefab;
        _physicsFactory = physicsFactory;
    }

    public Projectile Create(Vector3 position, Quaternion rotation)
    {
        ProjectileBehavior createdView =
            _diContainer.InstantiatePrefabForComponent<ProjectileBehavior>(_standardProjectilePrefab.gameObject, position, rotation, null);

        CustomPhysics physics = _physicsFactory.Create(createdView.transform.position, createdView.transform.rotation.eulerAngles.z);

        Projectile created = new Projectile(createdView, physics);
        _tickableManager.Add(created);
        return created;
    }
}