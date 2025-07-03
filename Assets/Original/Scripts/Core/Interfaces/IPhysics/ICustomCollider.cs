using Original.Scripts.Core.Physics;

namespace Original.Scripts.Core.Interfaces.IPhysics
{
    public interface ICustomCollider
    {
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