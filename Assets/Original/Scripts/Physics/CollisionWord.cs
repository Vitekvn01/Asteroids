using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWord
{
    private readonly List<CustomPhysics> _bodies = new();

    public void Register(CustomPhysics body) => _bodies.Add(body);
    public void Unregister(CustomPhysics body) => _bodies.Remove(body);

    public bool TryGetCorrection(CustomPhysics self, out Vector2 correction)
    {
        Debug.Log("TryGetCorrection");
        
        foreach (var other in _bodies)
        {
            if (other == self) continue;

            float dist = Vector2.Distance(self.Position, other.Position);
            float minDist = self.Radius + other.Radius;

            if (dist < minDist)
            {
                Vector2 dir = (self.Position - other.Position).normalized;
                correction = dir * (minDist - dist);
                return true;
            }
        }
        

        correction = Vector2.zero;
        return false;
    }
    
    public void ResolveCollision(CustomPhysics a, CustomPhysics b)
    {
        Vector2 delta = a.Position - b.Position;
        float dist = delta.magnitude;
        float minDist = a.Radius + b.Radius;

        if (dist < minDist)
        {
            Vector2 normal = delta.normalized;
            float penetration = minDist - dist;
            Vector2 correction = normal * (penetration / 2f);

            a.ApplyCollision(correction, normal);
            b.ApplyCollision(-correction, -normal);
        }
    }
}
