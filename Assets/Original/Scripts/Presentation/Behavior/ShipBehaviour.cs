using System;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour, IShipView
{
    [SerializeField] private Transform _shootPoint;
    
    
    public float RadiusCollider { get; }

    public Transform Transform => gameObject.transform;
    public Transform ShootPoint => _shootPoint;

    public event Action OnDestroyEvent;

    public void Death()
    {
        Destroy(gameObject);
    }
        
    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke();
    }

    
}
