using Original.Scripts.Core.Interfaces.IPhysics;

namespace Original.Scripts.Core.Physics
{
    public class CustomCollider : ICustomCollider
    {
        public bool IsTrigger { get; }
        public float Radius { get; }
        
        public PhysicsLayer Layer { get; private set;}
        
        public PhysicsLayer CollisionMask { get; private set; }
        public IColliderHandler Handler { get; private set; }
        
        public CustomCollider(float radius, bool isTrigger = false,
            PhysicsLayer layer = PhysicsLayer.Default, PhysicsLayer collisionMask = PhysicsLayer.All,
            IColliderHandler handler = null)
        {
            Radius = radius;
            IsTrigger = isTrigger;
            Layer = layer;
            CollisionMask = collisionMask;
            Handler = handler;
        }

        public void SetHandler(IColliderHandler handler)
        {
            Handler = handler;
        }

        public void SetLayer(PhysicsLayer layer)
        {
            Layer = layer;
        }

        public void SetCollisionMask(PhysicsLayer collisionMask)
        {
            CollisionMask = collisionMask;
        }

        public void OnTriggerEnter(ICustomCollider other) => 
            Handler?.OnTriggerEnter(other);

        public void OnCollisionEnter(ICustomCollider other) => 
            Handler?.OnCollisionEnter(other);
    }
}