using UnityEngine;

namespace Original.Scripts.Core.Physics
{
    public interface IColliderHandler
    {
        CustomPhysics Physics{ get;}
        void OnTriggerEnter(ICustomCollider other);

        void OnCollisionEnter(ICustomCollider other);
    }
}