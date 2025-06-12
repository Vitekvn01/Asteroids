using Original.Scripts.Core.Physics;
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
    public ShipFactory(DiContainer diContainer, TickableManager tickableManager, ShipBehaviour shipBehaviourPrefab,
        IWeaponFactory weaponFactory, ICustomPhysicsFactory physicsFactory, IInput input)
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
        var behaviour = _diContainer.InstantiatePrefabForComponent<ShipBehaviour>(_shipBehaviourPrefab.gameObject,
            pos, Quaternion.Euler(0,0, rot), null);

        bool isTrigger = false;
        bool isActive = true;
        ICustomCollider customCollider = new CustomCollider(behaviour.RadiusCollider, isTrigger, isActive);
        
        CustomPhysics customPhysics = _physicsFactory.Create(pos, behaviour.transform.rotation.eulerAngles.z, 1f, 5f,
            customCollider);
        
        IWeapon standardWeapon = _weaponFactory.Create(WeaponType.StandardWeapon);
        IWeapon laserWeapon = _weaponFactory.Create(WeaponType.LaserWeapon);
        
        var ship = new Ship( standardWeapon, laserWeapon);
        var movement = new ShipMovement(behaviour, ship, customPhysics);
        var controller = new ShipController(_input, ship, behaviour, movement);
        
        customCollider.SetHandler(ship);
        
        _tickableManager.Add(controller);
        
        return controller;
    }
}
