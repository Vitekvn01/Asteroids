using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Signals;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.PlayerShip
{
    public class ShipController : ITickable, IDisposable
    {
        private readonly IInput _input;
        
        private readonly IShipView _shipView;
        private readonly Ship _ship;
        private readonly ShipMovement _shipMovement;

        private readonly SignalBus _signalBus; 
        


        public Ship Ship => _ship;
        public ShipMovement ShipMovement => _shipMovement;
    
        public ShipController(IInput input, Ship ship, IShipView view, ShipMovement shipMovement, SignalBus signalBus)
        {
            _input = input;
        
            _shipView = view;
            _ship = ship;
            _shipMovement = shipMovement;
            _signalBus = signalBus;

            _ship.OnDeathEvent += OnDeathEvent;
        }
        public void Tick()
        {
            if (_ship.IsActive)
            {
                float axisX = _input.GetAxisX();
                float axisY = _input.GetAxisY();
        
                _shipMovement.Move(axisY, -axisX);
        
                _ship.Update();

                if (_input.CheckPressedFireSecondary())
                {
                    Debug.DrawRay(_shipView.ShootPoint.position, _shipView.ShootPoint.up);
                    _ship.PrimaryWeapon.TryShoot(_shipView.ShootPoint.position,_shipView.ShootPoint.rotation, _shipMovement.Physics.Velocity.magnitude);
                }

                if (_input.CheckPressedFirePrimary())
                {
                    _ship.SecondaryWeapon.TryShoot(_shipView.ShootPoint.position,_shipView.ShootPoint.rotation, _shipMovement.Physics.Velocity.magnitude);
                }
            }
        }

        private void OnDeathEvent()
        {
            _signalBus.Fire(new PlayerDeadSignal());
        }
    
        public void Dispose()
        {
            _ship.OnDeathEvent -= OnDeathEvent;
        }
    }
}
