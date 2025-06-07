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

    public CustomPhysics(Vector2 startPos, float radius, float startRot, PhysicsSettings physicsSettings, CollisionWord collisionWord)
    {
        _position = startPos;
        _rotation = startRot;
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
        
        if (_collisionWord.TryGetCorrection(this, out Vector2 correction))
        {
            HandleCollision(correction);
            Debug.Log("TryGetCorrection == true");
        }
    }
    
    public void ApplyCollision(Vector2 correction, Vector2 normal)
    {
        _position += correction;
        _velocity = Vector2.Reflect(_velocity, normal) * _physicsSettings.Bounce;
    }


    public void SetRotation(float angle)
    {
        _rotation = angle;
    }
    
    public void SetPostion(Vector2 pos)
    {
        _position = pos;
    }

    public void Rotate(float deltaAngle)
    {
        _rotation += deltaAngle;
    }
    
    private void HandleCollision(Vector2 correction)
    {
        Debug.Log("HandleCollision");
        _position += correction;

        // Отражаем скорость от поверхности столкновения
        Vector2 normal = correction.normalized;
        _velocity = Vector2.Reflect(_velocity, normal);

        // Можно также добавить небольшое трение или потерю энергии:
        // _velocity *= 0.8f;
    }
}
