using System.Collections.Generic;
using UnityEngine;

public class CollisionWord
{
    /*private readonly List<CustomPhysics> _bodies = new();

    public void Register(CustomPhysics body) => _bodies.Add(body);
    public void Unregister(CustomPhysics body) => _bodies.Remove(body);

    public void ResolveAll()
    {
        for (int i = 0; i < _bodies.Count; i++)
        {
            for (int j = i + 1; j < _bodies.Count; j++)
            {
                ResolveCollision(_bodies[i], _bodies[j]);
            }
        }
    }

    private void ResolveCollision(CustomPhysics a, CustomPhysics b)
    {
        Vector2 delta = a.Position - b.Position;
        float dist = delta.magnitude;
        float minDist = a.Radius + b.Radius;

        if (dist >= minDist)
            return;

        Vector2 normal = delta.normalized;
        float penetration = minDist - dist;

        // Сдвигаем их друг от друга
        Vector2 correction = normal * (penetration / 2f);
        a.SetPosition(a.Position + correction);
        b.SetPosition(b.Position - correction);

        // Отражаем скорость от нормали
        a.SetVelocity(Vector2.Reflect(a.Velocity, normal) * a.Bounce);
        b.SetVelocity(Vector2.Reflect(b.Velocity, -normal) * b.Bounce);
    }*/
}
