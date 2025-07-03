using Original.Scripts.Core.Interfaces.IView;
using UnityEngine;

namespace Original.Scripts.Presentation.Behavior
{
    public abstract class AbstractProjectileBehaviour : MonoBehaviour, IProjectileView
    {
        [SerializeField] private float _radiusCollider;
        public Transform Transform => gameObject.transform;
        public float RadiusCollider => _radiusCollider;
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}