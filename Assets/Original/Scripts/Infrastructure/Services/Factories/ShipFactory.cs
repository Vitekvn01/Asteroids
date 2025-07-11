using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Interfaces.IPhysics;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;
using Original.Scripts.Presentation.Behavior;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class ShipFactory : IShipFactory
    {
        private readonly DiContainer _diContainer;
        private readonly TickableManager _tickableManager;
        private readonly SignalBus _signalBus;
        
        private readonly IConfigProvider _configLoader;

        private readonly ShipBehaviour _shipBehaviourPrefab;
    
        private readonly IWeaponFactory _weaponFactory;
        private readonly ICustomPhysicsFactory _physicsFactory;
    
        private readonly IInput _input;
 
    
        [Inject]
        public ShipFactory(DiContainer diContainer, TickableManager tickableManager, SignalBus signalBus,
            IConfigProvider configLoader, ShipBehaviour shipBehaviourPrefab, IWeaponFactory weaponFactory,
            ICustomPhysicsFactory physicsFactory, IInput input)
        {
            _tickableManager = tickableManager;
            _diContainer = diContainer;
            _configLoader = configLoader;
            _shipBehaviourPrefab = shipBehaviourPrefab;
            _weaponFactory = weaponFactory;
            _physicsFactory = physicsFactory;
            _input = input;
            _signalBus = signalBus;
        }
    
        public ShipController Create(Vector2 pos, float rot = 0)
        {
            var behaviour = _diContainer.InstantiatePrefabForComponent<ShipBehaviour>(_shipBehaviourPrefab.gameObject,
                pos, Quaternion.Euler(0,0, rot), null);

            bool isTrigger = false;
            PhysicsLayer layer = PhysicsLayer.Player;
            PhysicsLayer collisionMask = PhysicsLayer.Enemy;
            ICustomCollider customCollider = new CustomCollider(behaviour.RadiusCollider, 
                isTrigger, layer, collisionMask);
        
            CustomPhysics customPhysics = _physicsFactory
                .Create(pos, behaviour.transform.rotation.eulerAngles.z, 1f, 5f, customCollider);
        
            IWeapon standardWeapon = _weaponFactory.Create(WeaponType.StandardWeapon);
            IWeapon laserWeapon = _weaponFactory.Create(WeaponType.LaserWeapon);
        
            var ship = new Ship( standardWeapon, laserWeapon, customPhysics, behaviour, _configLoader.PlayerConfig);
            var movement = new ShipMovement(behaviour, ship, customPhysics);
            var controller = new ShipController(_input, ship, behaviour, movement, _signalBus);
        
            customCollider.SetHandler(ship);
            
            _tickableManager.Add(controller);
            
            return controller;
        }
    }
}
