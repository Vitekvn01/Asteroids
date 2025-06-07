using UnityEngine;
using Zenject;

public class ShipFactory : IShipFactory
{
    private readonly DiContainer _diContainer;
    private readonly TickableManager _tickableManager;
    
    private readonly ShipBehaviour _shipBehaviourPrefab;
    private readonly IWeaponFactory _weaponFactory;
    private readonly ICustomPhysicsFactory _physicsFactory;
    private readonly IInput _input;
    
    [Inject]
    public ShipFactory(DiContainer diContainer, TickableManager tickableManager, ShipBehaviour shipBehaviourPrefab, IWeaponFactory weaponFactory, ICustomPhysicsFactory physicsFactory, IInput input)
    {
        _tickableManager = tickableManager;
        _diContainer = diContainer;
        _shipBehaviourPrefab = shipBehaviourPrefab;
        _weaponFactory = weaponFactory;
        _physicsFactory = physicsFactory;
        _input = input;
    }
    
    public ShipController Create(Vector2 pos, float rot = 0)
    {
        var behaviour = _diContainer.InstantiatePrefabForComponent<ShipBehaviour>(_shipBehaviourPrefab.gameObject, pos, Quaternion.Euler(0,0, rot), null);

        IWeapon standardWeapon = _weaponFactory.Create(WeaponType.StandardWeapon, behaviour.ShootPoint);
        IWeapon laserWeapon = _weaponFactory.Create(WeaponType.LaserWeapon, behaviour.ShootPoint);

        CustomPhysics customPhysics = _physicsFactory.Create(pos, rot, behaviour.transform.localScale.x);
        
        var ship = new Ship(behaviour, standardWeapon, laserWeapon);
        var movement = new ShipMovement(behaviour, customPhysics, 10, 100);
        var controller = new ShipController(_input, ship, behaviour, movement);
        
        _tickableManager.Add(controller);
        
        return controller;
    }
}
