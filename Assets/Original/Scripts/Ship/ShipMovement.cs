using UnityEngine;
using Zenject;

public class ShipMovement
{
    private readonly ShipBehaviour _shipBehaviour;
    private readonly CustomPhysics _physics;
    
    private readonly float _moveSpeed;
    private readonly float _rotationSpeed;
    
    [Inject]
    public ShipMovement(ShipBehaviour shipBehaviour, ICustomPhysicsFactory physicsFactory, float moveSpeed, float rotationSpeed)
    {
        _shipBehaviour = shipBehaviour;
        _moveSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
        
        _physics = physicsFactory.Create(_shipBehaviour.transform.position, 0);
    }
    
    public void Move(float moveInput, float rotationInput)
    {
        float deltaAngle = rotationInput * _rotationSpeed * Time.deltaTime;
        _physics.Rotate(deltaAngle);
        
        Vector2 forward = _shipBehaviour.transform.up;
        Vector2 force = forward * (moveInput * _moveSpeed);
        _physics.AddForce(force);
        
        _shipBehaviour.transform.position = _physics.Position;
        _shipBehaviour.transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);
    }
}
