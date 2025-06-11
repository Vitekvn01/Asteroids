namespace Original.Scripts.Core.Physics
{
    public interface IColliderHandler
    {
        void OnTriggerEnter(ICustomCollider other);

        void OnCollisionEnter(ICustomCollider other);
    }
}