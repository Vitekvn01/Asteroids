using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CustomPhysics : ITickable
{
    private Vector2 _position;
    private Vector2 _velocity;
    private float _rotation;

    private PhysicsSettings _physicsSettings;

    private Vector2 _accumulatedForce;
    
    public Vector2 Position => _position;
    public Vector2 Velocity => _velocity;
    public float Rotation => _rotation;

    public CustomPhysics(Vector2 startPos, PhysicsSettings physicsSettings)
    {
        _position = startPos;
        _physicsSettings = physicsSettings;
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

    public void SetRotation(float angle)
    {
        _rotation = angle;
    }

    public void Rotate(float deltaAngle)
    {
        _rotation += deltaAngle;
    }
}
