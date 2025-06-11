using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

public class CustomPhysics : ITickable
{
    private readonly PhysicsSettings _physicsSettings;
    
    private  ICustomCollider _collider;

    private bool _isActive;
    
    private Vector2 _position;
    private Vector2 _velocity;
    private Vector2 _accumulatedForce;
    
    private float _rotation;

    private float _radius;

    public ICustomCollider Collider => _collider;

    public bool IsActive => _isActive; 
    public Vector2 Position => _position;
    public Vector2 Velocity => _velocity;
    public float Rotation => _rotation;

    public CustomPhysics(Vector2 startPos, float startRot, ICustomCollider customCollider, PhysicsSettings physicsSettings)
    {
        _position = startPos;
        _rotation = startRot;
        _collider = customCollider;
        _physicsSettings = physicsSettings;
        _isActive = true;
    }

    public void Tick()
    {
        if (_isActive)
        {
            float deltaTime = Time.deltaTime;
        
            _velocity += _accumulatedForce * deltaTime;
        
            _velocity = Vector2.Lerp(_velocity, Vector2.zero, _physicsSettings.Drag * deltaTime);
        
            if (_velocity.magnitude > _physicsSettings.MaxSpeed)
                _velocity = _velocity.normalized * _physicsSettings.MaxSpeed;

            _position += _velocity * deltaTime;
            _accumulatedForce = Vector2.zero;
        }
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }
    
    public void AddForce(Vector2 force)
    {
        _accumulatedForce += force;
    }
    public void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }
    public void SetRotation(float angle)
    {
        _rotation = angle;
    }
    
    public void SetPosition(Vector2 pos)
    {
        _position = pos;
    }

    public void Rotate(float deltaAngle)
    {
        _rotation += deltaAngle;
    }
}
