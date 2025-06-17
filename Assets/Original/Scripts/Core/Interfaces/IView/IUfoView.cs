using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IView
{
    public interface IUfoView : IPhysicsView, IView
    {
        public Transform ShootPoint { get; }
    }
}