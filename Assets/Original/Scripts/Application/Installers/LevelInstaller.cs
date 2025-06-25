using Original.Scripts.Application.Gameplay.Spawner;
using Original.Scripts.Core;
using Original.Scripts.Core.Config;
using Original.Scripts.Core.Entity;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.Signals;
using Original.Scripts.Infrastructure.ObjectPool;
using Original.Scripts.Infrastructure.Services;
using Original.Scripts.Infrastructure.Services.Factories;
using Original.Scripts.Infrastructure.Services.Input;
using Original.Scripts.Presentation.UI.View;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private ProjectileBehaviour _projectilePrefab;
    [SerializeField] private ProjectileBehaviour _laserProjectilePrefab;
    [SerializeField] private ShipBehaviour _shipPrefab;
    [SerializeField] private AsteroidBehaviour _asteroidPrefab;
    [SerializeField] private AsteroidBehaviour _debrisPrefab;
    [SerializeField] private UfoBehaviour _ufoBehaviour;


    public override void InstallBindings()
    {
        BindFactories();
        BindPrefabs();
        BindSettings();
        BindServices();
        BindGameLogic();
        BindPools();
        BindSignals();
        
        Container.BindInterfacesTo<Game>().AsSingle();
    }
    


    private void BindPrefabs()
    {
        Container.Bind<ShipBehaviour>()
            .FromInstance(_shipPrefab);

        Container.Bind<ProjectileBehaviour>()
            .WithId("BulletProjectile")
            .FromInstance(_projectilePrefab);

        Container.Bind<ProjectileBehaviour>()
            .WithId("LaserProjectile")
            .FromInstance(_laserProjectilePrefab);

        Container.Bind<AsteroidBehaviour>()
            .WithId("Asteroid")
            .FromInstance(_asteroidPrefab);
        
        Container.Bind<AsteroidBehaviour>()
            .WithId("Debris")
            .FromInstance(_debrisPrefab);
        
        Container.Bind<UfoBehaviour>()
            .FromInstance(_ufoBehaviour);
    }

    private void BindSettings()
    {
        Container.Bind<PhysicsSettings>()
            .AsSingle()
            .WithArguments( 100f);
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

        Container.Bind<IUIFactory>()
            .To<UIFactory>()
            .AsSingle();
    }

    private void BindGameLogic()
    {
        Container.BindInterfacesAndSelfTo<PlayerSpawner>()
            .AsSingle();

        Container.Bind<EnemySpawner>()
            .AsSingle();

        Container.Bind<IInput>()
            .To<DesktopInput>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<CollisionWord>()
            .AsSingle();

        Camera main = Camera.main;
        
        Container.Bind<WorldBounds>()
            .AsSingle()
            .WithArguments(100f, main);
    }

    private void BindPools()
    {
        Container.Bind<IProjectilePool>()
            .To<ProjectilePool>()
            .AsSingle();

        Container.Bind<IEnemyPool>()
            .To<EnemyPool>()
            .AsSingle();
    }
    

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<EnemyDestroyedSignal>();
        
        Container.DeclareSignal<PlayerDeadSignal>();
        
        Container.DeclareSignal<StartGameSignal>();
        

    }
    


    private void BindServices()
    {
        Container.Bind<IConfigProvider>()
            .To<ConfigLoader>()
            .AsSingle();
        
        Container.Bind<IRewardSystem>().To<RewardSystem>().AsSingle();
        
        Container.Bind<IScore>().To<Score>().AsSingle();
        
        Container.BindInterfacesTo<RewardHandler>().AsSingle();
    }
}