using UnityEngine;

namespace Original.Scripts.Core.Physics
{
    public interface ICustomCollider
    {
        bool IsActive { get; }
        bool IsTrigger { get; }
        
        float Radius { get; }
        IColliderHandler Handler { get; }
        
        PhysicsLayer Layer { get; }
        
        PhysicsLayer CollisionMask { get; }

        void SetHandler(IColliderHandler handler);

        void SetLayer(PhysicsLayer layer);

        void SetCollisionMask(PhysicsLayer collisionMask);
        
        void OnTriggerEnter(ICustomCollider other);

        void OnCollisionEnter(ICustomCollider other);
    }
}