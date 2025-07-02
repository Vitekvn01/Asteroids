using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Signals;
using Zenject;

namespace Original.Scripts.Core.Entity.PlayerShip
{
    public class ShipController : ITickable, IDisposable
    {
        private readonly IInput _input;

        public IShipView ShipView { get; }
        public Ship Ship { get; }
        public ShipMovement ShipMovement { get; }

        private readonly SignalBus _signalBus; 
    
        public ShipController(IInput input, Ship ship, IShipView view, ShipMovement shipMovement, SignalBus signalBus)
        {
            _input = input;
        
            ShipView = view;
            Ship = ship;
            ShipMovement = shipMovement;
            _signalBus = signalBus;

            Ship.OnDeathEvent += OnDeathEvent;
        }
        public void Tick()
        {
            if (Ship.IsActive)
            {
                float axisX = _input.GetAxisX();
                float axisY = _input.GetAxisY();
        
                Movement(axisY, axisX);
                Fire();
            }
        }

        private void Movement(float axisY, float axisX)
        {
            ShipMovement.Move(axisY, -axisX);

            Ship.Update();
        }

        private void Fire()
        {
            if (_input.CheckPressedFireSecondary())
            {
                Ship.PrimaryWeapon.TryShoot(ShipView.ShootPoint.position,ShipView.ShootPoint.rotation, ShipMovement.Physics.Velocity.magnitude);
            }

            if (_input.CheckPressedFirePrimary())
            {
                Ship.SecondaryWeapon.TryShoot(ShipView.ShootPoint.position,ShipView.ShootPoint.rotation, ShipMovement.Physics.Velocity.magnitude);
            }
        }

        private void OnDeathEvent()
        {
            _signalBus.Fire(new PlayerDeadSignal());
        }
    
        public void Dispose()
        {
            Ship.OnDeathEvent -= OnDeathEvent;
        }
    }
}
