using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollisionWord : IFixedTickable
{
    private readonly List<CustomPhysics> _bodies = new();

    private readonly PhysicsSettings _physicsSettings;
    
    [Inject]
    public CollisionWord(PhysicsSettings physicsSettings)
    {
        _physicsSettings = physicsSettings;
    }

    public void Register(CustomPhysics body) => _bodies.Add(body);
    public void Unregister(CustomPhysics body) => _bodies.Remove(body);
    
    public void FixedTick()
    {
        Debug.Log("FixedTick");
        ResolveAll();
    }
    private void ResolveAll()
    {
        for (int i = 0; i < _bodies.Count; i++)
        {
            for (int j = i + 1; j < _bodies.Count; j++)
            {
                if (_bodies[i].IsActive && _bodies[j].IsActive)
                {
                    ResolveCollision(_bodies[i], _bodies[j]); 
                }
            }
        }
    }

    private void ResolveCollision(CustomPhysics a, CustomPhysics b)
    {
        float dist = Vector2.Distance(a.Position, b.Position);
        
        float minDist = a.Collider.Radius + b.Collider.Radius;
        
        if (dist <= minDist)
        {
            if (a.Collider.IsTrigger || b.Collider.IsTrigger)
            {
                if (a.Collider.IsTrigger)
                {
                    a.Collider.OnTriggerEnter(b.Collider);
                }
                
                if (b.Collider.IsTrigger)
                {
                    b.Collider.OnTriggerEnter(a.Collider);
                }
            }
            else
            {
                Vector2 delta = a.Position - b.Position;
                Vector2 normal = delta.normalized;
                float penetration = minDist - dist;
                
                a.Collider.OnCollisionEnter(b.Collider);
                b.Collider.OnCollisionEnter(a.Collider);
                
                
                Vector2 correction = normal * (penetration / 2f);
                a.SetPosition(a.Position + correction);
                b.SetPosition(b.Position - correction);
                
                a.SetVelocity(Vector2.Reflect(a.Velocity, normal) * _physicsSettings.Bounce);
                b.SetVelocity(Vector2.Reflect(b.Velocity, -normal) * _physicsSettings.Bounce);
            }
        }

    }

 
}
