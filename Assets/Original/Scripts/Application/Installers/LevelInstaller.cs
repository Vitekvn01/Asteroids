using Original.Scripts.Core;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private ProjectileBehavior _projectilePrefab;
    [SerializeField] private ShipBehaviour _shipPrefab;

    public override void InstallBindings()
    {
        BindPrefabs();
        BindSettings();
        BindFactories();
        BindGameLogic();
        BindPools();
    }

    private void BindPrefabs()
    {
        Container.Bind<ShipBehaviour>()
            .FromInstance(_shipPrefab)
            .AsSingle();

        Container.Bind<ProjectileBehavior>()
            .FromInstance(_projectilePrefab)
            .AsSingle();
    }

    private void BindSettings()
    {
        Container.Bind<PhysicsSettings>()
            .AsSingle()
            .WithArguments( 10f);
    }

    private void BindFactories()
    {
        Container.Bind<ICustomPhysicsFactory>()
            .To<CustomPhysicsFactory>()
            .AsSingle();

        Container.Bind<IWeaponFactory>()
            .To<WeaponFactory>()
            .AsSingle();

        Container.Bind<IProjectileFactory>()
            .To<ProjectileFactory>()
            .AsSingle();

        Container.Bind<IShipFactory>()
            .To<ShipFactory>()
            .AsSingle();
    }

    private void BindGameLogic()
    {
        Container.BindInterfacesAndSelfTo<PlayerSpawner>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IInput>()
            .To<DesktopInput>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<CollisionWord>()
            .AsSingle();

        Camera main = Camera.main;
        
        Container.Bind<WorldBounds>()
            .AsSingle()
            .WithArguments(main);
    }

    private void BindPools()
    {
        Container.Bind<IObjectPool<Projectile>>()
            .To<ProjectilePool>()
            .AsSingle();
    }

    private void BindShipLogic()
    {

    }
}