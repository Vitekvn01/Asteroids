using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IView
{
    public interface IShipView : IPhysicsView
    {
        public Transform ShootPoint { get; }
        public void Death();

    }
}
