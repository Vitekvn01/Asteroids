using System;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;

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
