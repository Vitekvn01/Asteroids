using UnityEngine;

namespace Original.Scripts.Core.Physics
{
    public interface ICustomCollider
    {
        bool IsActive { get; }
        bool IsTrigger { get; }
        
        float Radius { get; }
        IColliderHandler Owner { get; }

        void OnTriggerEnter(ICustomCollider other);

        void OnCollisionEnter(ICustomCollider other);
    }
}