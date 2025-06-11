namespace Original.Scripts.Core.Physics
{
    public class CustomCollider : ICustomCollider
    {
        public bool IsActive { get; }
        public bool IsTrigger { get; }
        public IColliderHandler Handler { get; private set; }
        public float Radius { get; }
        

        public CustomCollider(float radius, bool isTrigger = false, bool isActive = true, IColliderHandler handler = null)
        {
            Radius = radius;
            IsTrigger = isTrigger;
            IsActive = isActive;
            Handler = handler;
        }

        public void SetHandler(IColliderHandler handler)
        {
            Handler = handler;
        }

        public void OnTriggerEnter(ICustomCollider other) => 
            Handler?.OnTriggerEnter(other);

        public void OnCollisionEnter(ICustomCollider other) => 
            Handler?.OnCollisionEnter(other);
    }
}