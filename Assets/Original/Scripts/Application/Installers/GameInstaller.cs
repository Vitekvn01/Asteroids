using Original.Scripts.Application.Gameplay;
using Original.Scripts.Application.Gameplay.Spawner;
using Original.Scripts.Core;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.Signals;
using Original.Scripts.Core.Signals.inputSignal;
using Original.Scripts.Infrastructure.Mock;
using Original.Scripts.Infrastructure.ObjectPool;
using Original.Scripts.Infrastructure.Services;
using Original.Scripts.Infrastructure.Services.Factories;
using Original.Scripts.Infrastructure.Services.Inputs;
using Original.Scripts.Presentation.Behavior;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Application.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        
        [SerializeField] private ProjectileBehaviour _projectilePrefab;
        [SerializeField] private LaserProjectileBehavior _laserProjectilePrefab;
        [SerializeField] private ShipBehaviour _shipPrefab;
        [SerializeField] private AsteroidBehaviour _asteroidPrefab;
        [SerializeField] private DebrisBehaviour _debrisPrefab;
        [SerializeField] private UfoBehaviour _ufoBehaviour;


        public override void InstallBindings()
        {
            Container.Bind<Camera>()
                .FromInstance(_mainCamera);
            
            BindFactories();
            BindPrefabs();
            BindSettings();
            BindServices();
            BindGameLogic();
            BindPools();
            BindSignals();
            BindInput();

            Container.BindInterfacesTo<Game>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
        }
    


        private void BindPrefabs()
        {
            Container.Bind<ShipBehaviour>()
                .FromInstance(_shipPrefab);

            Container.Bind<ProjectileBehaviour>()
                .FromInstance(_projectilePrefab);

            Container.Bind<LaserProjectileBehavior>()
                .FromInstance(_laserProjectilePrefab);

            Container.Bind<AsteroidBehaviour>()
                .FromInstance(_asteroidPrefab);
        
            Container.Bind<DebrisBehaviour>()
                .FromInstance(_debrisPrefab);
        
            Container.Bind<UfoBehaviour>()
                .FromInstance(_ufoBehaviour);
        }

        private void BindSettings()
        {
            Container.Bind<PhysicsSettings>()
                .AsSingle();
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
            Container.Bind<PlayerSpawner>()
                .AsSingle();

            Container.Bind<EnemySpawner>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<CollisionWord>()
                .AsSingle();

            Container.Bind<WorldBounds>()
                .AsSingle();
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

            Container.DeclareSignal<JoystickDirectionSignal>();
        
            Container.DeclareSignal<ShootButtonSignal>();
        }
    


        private void BindServices()
        {
            Container.BindInterfacesTo<ManualFixedTickManager>()
                .AsSingle();
            
            Container.Bind<IConfigLoader>()
                .To<ConfigLoader>()
                .AsSingle();
            
            Container.Bind<IConfigProvider>()
                .To<ConfigProvider>()
                .AsSingle();
            
            Container.Bind<IRewardSystem>().
                To<RewardSystem>()
                .AsSingle();
        
            Container.Bind<IScore>()
                .To<Score>()
                .AsSingle();
        
            Container.BindInterfacesTo<RewardHandler>()
                .AsSingle();

#if UNITY_EDITOR
            Container.Bind<IAdsStrategy>()
                .To<MockAdsService>()
                .AsSingle();
#else
             Container.Bind<IAdsStrategy>()
                 .To<AdsService>()
                 .AsSingle();
#endif

#if UNITY_EDITOR
            Container.BindInterfacesTo<MockAnalyticsService>().AsSingle();
#else
            Container.BindInterfacesTo<AnalyticsService>().AsSingle();
#endif
        }

        private void BindInput()
        {
            if (UnityEngine.Application.platform == RuntimePlatform.Android)
            {
                Container.Bind<IInput>()
                    .To<MobileInput>()
                    .AsSingle();
            }
            
            if (UnityEngine.Application.platform == RuntimePlatform.WindowsEditor)
            {
                Container.Bind<IInput>()
                    .To<DesktopInput>()
                    .AsSingle();
            }

            if (UnityEngine.Application.platform == RuntimePlatform.XboxOne)
            {
                Container.Bind<IInput>()
                    .To<XboxInput>()
                    .AsSingle();
            }
        }
    }
}