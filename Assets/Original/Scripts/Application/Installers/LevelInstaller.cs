using Original.Scripts.Core;
using Original.Scripts.Core.Config;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using Original.Scripts.Infrastructure.ObjectPool;
using Original.Scripts.Infrastructure.Services;
using Original.Scripts.Infrastructure.Services.Factories;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private ProjectileBehaviour _projectilePrefab;
    [SerializeField] private ShipBehaviour _shipPrefab;
    [SerializeField] private AsteroidBehaviour _asteroidPrefab;

    public override void InstallBindings()
    {
        BindConfigs();
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

        Container.Bind<ProjectileBehaviour>()
            .FromInstance(_projectilePrefab)
            .AsSingle();

        Container.Bind<AsteroidBehaviour>()
            .FromInstance(_asteroidPrefab)
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

        Container.Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();
    }

    private void BindGameLogic()
    {
        Container.BindInterfacesAndSelfTo<PlayerSpawner>()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<EnemySpawner>()
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

        Container.Bind<IEnemyPool>()
            .To<EnemyPool>()
            .AsSingle();
    }


    private void BindConfigs()
    {
        Container.Bind<IConfigProvider>()
            .To<ConfigLoader>()
            .AsSingle();
    }
}