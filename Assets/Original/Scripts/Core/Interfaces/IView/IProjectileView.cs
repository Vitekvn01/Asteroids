using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IView
{
    public interface IProjectileView : IPhysicsView, IView
    {
        public void SetActive(bool isActive);
    }
}