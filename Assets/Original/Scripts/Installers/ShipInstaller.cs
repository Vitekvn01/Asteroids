using UnityEngine;
using Zenject;

public class ShipInstaller : MonoInstaller
{
    [SerializeField] private ShipBehaviour _shipPrefab;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 180f;
    
    public override void InstallBindings()
    {
        Container.Bind<ShipBehaviour>()
            .FromComponentInNewPrefab(_shipPrefab)
            .AsSingle()
            .NonLazy();

 
        Container.Bind<Ship>()
            .AsSingle();


        Container.Bind<IInput>()
            .To<DesktopInput>()
            .AsSingle();
        

        Container.Bind<ShipMovement>()
            .AsSingle()
            .WithArguments(
                _moveSpeed,    
                _rotationSpeed
            );
        
        Container.BindInterfacesAndSelfTo<ShipController>()
            .AsSingle()
            .NonLazy();

        Container.Bind<ICustomPhysicsFactory>()
            .To<CustomPhysicsFactory>()
            .AsSingle();

        Container.Bind<PhysicsSettings>()
            .AsSingle()
            .WithArguments(1f, 10f);

        Container.Bind<IWeapon>().WithId("LaserWeapon").To<LaserWeapon>()
            .AsSingle()
            .WithArguments(20, 0.5f);
        
        Container.Bind<IWeapon>().WithId("StandardWeapon").To<StandardWeapon>()
            .AsSingle()
            .WithArguments(0.1f);
    }
    
}