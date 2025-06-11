namespace Original.Scripts.Core.Physics
{
    public class CustomCollider : ICustomCollider
    {
        public bool IsTrigger { get; }
        public IColliderHandler Owner { get; }
        public float Radius { get; }
        

        public CustomCollider(IColliderHandler owner, float radius, bool isTrigger)
        {
            Owner = owner;
            Radius = radius;
            IsTrigger = isTrigger;
        }

        public void OnTriggerEnter(ICustomCollider other) => 
            Owner.OnTriggerEnter(other);

        public void OnCollisionEnter(ICustomCollider other) => 
            Owner.OnCollisionEnter(other);
    }
}