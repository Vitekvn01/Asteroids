using UnityEngine;
using Zenject;

public class ShipMovement
{
    private readonly ShipBehaviour _shipBehaviour;
    private readonly CustomPhysics _physics;
    private readonly ICustomPhysicsFactory _physicsFactory;
    
    private readonly float _moveSpeed;
    private readonly float _rotationSpeed;
    
    [Inject]
    public ShipMovement(ShipBehaviour shipBehaviour, ICustomPhysicsFactory physicsFactory, float moveSpeed, float rotationSpeed)
    {
        _shipBehaviour = shipBehaviour;
        _physicsFactory = physicsFactory;
        _moveSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;
        
        _physics = _physicsFactory.Create(_shipBehaviour.transform.position);
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
