using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IView
{
    public interface IProjectileView : IPhysicsView
    {
        public void SetActive(bool isActive);
    }
}