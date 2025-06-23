using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.PlayerShip
{
    public class ShipMovement
    {
        private readonly IShipView _shipView;
        private readonly Ship _ship;
        private readonly CustomPhysics _physics;

        public CustomPhysics Physics => _physics;
    
        [Inject]
        public ShipMovement(IShipView shipView, Ship ship, CustomPhysics physics)
        {
            _ship = ship;
            _shipView = shipView;
            _physics = physics;
        }
    
        public void Move(float moveInput, float rotationInput)
        {
            if (_ship.CanControl)
            {
                float deltaAngle = rotationInput * _ship.RotationSpeed  * Time.deltaTime;
                _physics.Rotate(deltaAngle);
                
                Vector2 forward = _shipView.Transform.up;
                Vector2 force = forward * (moveInput * _ship.MoveSpeed);
                _physics.AddForce(force);
                
            }
            
            _shipView.Transform.position = _physics.Position;
            _shipView.Transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);

            if (_physics.Velocity.magnitude > _ship.MaxSpeed)
            {
                Vector2 clampedVelocity = _physics.Velocity.normalized * _ship.MaxSpeed;
                _physics.SetVelocity(clampedVelocity);
            }
        }
    }
}
