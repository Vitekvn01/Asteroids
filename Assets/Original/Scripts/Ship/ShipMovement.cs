using UnityEngine;
using Zenject;

public class ShipMovement
{
    private readonly ShipBehaviour _shipBehaviour;
    private readonly Ship _ship;
    private readonly CustomPhysics _physics;
    
    [Inject]
    public ShipMovement(ShipBehaviour shipBehaviour, Ship ship, CustomPhysics physics)
    {
        _ship = ship;
        _shipBehaviour = shipBehaviour;
        _physics = physics;
    }
    
    public void Move(float moveInput, float rotationInput)
    {
        float deltaAngle = rotationInput * _ship.RotationSpeed  * Time.deltaTime;
        _physics.Rotate(deltaAngle);
        
        Vector2 forward = _shipBehaviour.transform.up;
        Vector2 force = forward * (moveInput * _ship.MoveSpeed);
        _physics.AddForce(force);
        
        _shipBehaviour.transform.position = _physics.Position;
        _shipBehaviour.transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);
    }
}
