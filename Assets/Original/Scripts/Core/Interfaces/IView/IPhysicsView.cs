

using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IView
{
    public interface IPhysicsView
    {
        public Transform Transform { get; }
        public float RadiusCollider { get; }
    }
}