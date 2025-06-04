using UnityEngine;
using Zenject;

public class ShipInstaller : MonoInstaller
{
    [SerializeField] private ProjectileBehavior _projectilePrefab;
    [SerializeField] private ShipBehaviour _shipPrefab;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 180f;

    public override void InstallBindings()
    {
        BindPrefabs();
        BindSettings();
        BindFactories();
        BindGameLogic();
    }

    private void BindPrefabs()
    {
        Container.Bind<ShipBehaviour>()
            .FromComponentInNewPrefab(_shipPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<ProjectileBehavior>()
            .FromInstance(_projectilePrefab)
            .AsSingle()
            .NonLazy();
    }

    private void BindSettings()
    {
        Container.Bind<PhysicsSettings>()
            .AsSingle()
            .WithArguments(1f, 10f);
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
    }

    private void BindGameLogic()
    {


        Container.Bind<IInput>()
            .To<DesktopInput>()
            .AsSingle();


    }

    private void BindShipLogic()
    {
        Container.Bind<Ship>()
            .AsSingle();
        
        Container.Bind<ShipMovement>()
            .AsSingle()
            .WithArguments(_moveSpeed, _rotationSpeed);

        Container.BindInterfacesAndSelfTo<ShipController>()
            .AsSingle()
            .NonLazy();
    }
}