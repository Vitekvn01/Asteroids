using Original.Scripts.Core.Interfaces.IView;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour, IAsteroidView
{
    [SerializeField] private float _radiusCollider;
    public Transform Transform => gameObject.transform;

    public float RadiusCollider => _radiusCollider;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusCollider);
    }
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
