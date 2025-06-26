using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IView
{
    public interface IShipView : IPhysicsView, IView
    {
        public Transform ShootPoint { get; }

        public void SetActiveDefenceEffect(bool isActive);
    }
}
