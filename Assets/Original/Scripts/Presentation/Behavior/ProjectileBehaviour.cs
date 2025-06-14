using Original.Scripts.Core.Interfaces.IView;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour, IProjectileView
{
    [SerializeField] private float _radiusCollider;
    public Transform Transform => gameObject.transform;

    public float RadiusCollider => _radiusCollider;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusCollider);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
