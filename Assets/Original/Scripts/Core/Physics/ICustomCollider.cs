using UnityEngine;

namespace Original.Scripts.Core.Physics
{
    public interface ICustomCollider
    {
        bool IsActive { get; }
        bool IsTrigger { get; }
        
        float Radius { get; }
        IColliderHandler Handler { get; }

        void SetHandler(IColliderHandler handler);
        
        void OnTriggerEnter(ICustomCollider other);

        void OnCollisionEnter(ICustomCollider other);
    }
}