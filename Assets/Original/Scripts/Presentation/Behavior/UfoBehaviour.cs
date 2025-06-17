using System.Collections;
using System.Collections.Generic;
using Original.Scripts.Core.Interfaces.IView;
using UnityEngine;

public class UfoBehaviour : MonoBehaviour, IUfoView
{
    [SerializeField] private float _radiusCollider;
    
    [SerializeField] private Transform _shootPoint;
    public Transform Transform => gameObject.transform;

    public float RadiusCollider => _radiusCollider;

    public Transform ShootPoint => _shootPoint;
    
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
