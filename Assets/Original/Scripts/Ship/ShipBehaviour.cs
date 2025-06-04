using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;

    public Transform ShootPoint => _shootPoint;
}
