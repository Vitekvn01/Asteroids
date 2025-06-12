using Original.Scripts.Core.Physics;
using UnityEngine;

public interface ICustomPhysicsFactory
{
    public CustomPhysics Create(Vector2 pos, float rotation, float drag, float bounce, ICustomCollider customCollider);
}