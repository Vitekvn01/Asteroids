using Original.Scripts.Core.Physics;

namespace Original.Scripts.Core.Interfaces.IPhysics
{
    public interface IColliderHandler
    {
        void OnTriggerEnter(ICustomCollider other);

        void OnCollisionEnter(ICustomCollider other);
    }
}