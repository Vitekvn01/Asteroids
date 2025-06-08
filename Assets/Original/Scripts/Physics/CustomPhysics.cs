using UnityEngine;
using Zenject;

public class CustomPhysics : ITickable
{
    private readonly PhysicsSettings _physicsSettings;
    private readonly CollisionWord _collisionWord;
    
    private Vector2 _position;
    private Vector2 _velocity;
    private Vector2 _accumulatedForce;
    
    private float _rotation;

    private float _radius;
    
    public Vector2 Position => _position;
    public Vector2 Velocity => _velocity;
    public float Rotation => _rotation;

    public float Radius => _radius;

    public CustomPhysics(Vector2 startPos, float startRot, float radius, PhysicsSettings physicsSettings, CollisionWord collisionWord)
    {
        _position = startPos;
        _rotation = startRot;
        Debug.Log("Rotation " + _rotation);
        _radius = radius;
        _physicsSettings = physicsSettings;
        _collisionWord = collisionWord;
    }

    public void AddForce(Vector2 force)
    {
        _accumulatedForce += force;
    }

    public void Tick()
    {
        float deltaTime = Time.deltaTime;
        
        _velocity += _accumulatedForce * deltaTime;
        
        _velocity = Vector2.Lerp(_velocity, Vector2.zero, _physicsSettings.Drag * deltaTime);
        
        if (_velocity.magnitude > _physicsSettings.MaxSpeed)
            _velocity = _velocity.normalized * _physicsSettings.MaxSpeed;

        _position += _velocity * deltaTime;
        _accumulatedForce = Vector2.zero;
        
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
    
    private void HandleCollision(Vector2 correction)
    {

    }
}
