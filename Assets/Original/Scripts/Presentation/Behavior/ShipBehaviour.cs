using System;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Interfaces.IView;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour, IShipView
{
    [SerializeField] private float _radiusCollider;
    
    [SerializeField] private Transform _shootPoint;
    
    public float RadiusCollider { get; }

    public Transform Transform => gameObject.transform;
    public Transform ShootPoint => _shootPoint;

    public event Action OnDestroyEvent;
    
    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke();
    }

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
