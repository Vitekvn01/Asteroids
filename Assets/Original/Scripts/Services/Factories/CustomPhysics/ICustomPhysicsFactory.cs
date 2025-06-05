using UnityEngine;

public interface ICustomPhysicsFactory
{
    public CustomPhysics Create(Vector2 pos, float rotation);
}