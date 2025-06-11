using System;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

public class Ship : IColliderHandler
{
    private const int StartHealth = 3;
    private const int MaxRemoveHealth = 1;
    
    private readonly float _moveSpeed = 10;
    private readonly float _rotationSpeed = 100;
    
    private int _health;
    
    private float _timerTest;
    
    private IWeapon _primaryWeapon;
    private IWeapon _secondaryWeapon;
    
    public float MoveSpeed => _moveSpeed;
    public float RotationSpeed => _rotationSpeed;
    
    public IWeapon PrimaryWeapon => _primaryWeapon;
    public IWeapon SecondaryWeapon => _secondaryWeapon;
    

    public event Action<int> OnHealthChangedEvent;
    public event Action OnDeathEvent;
    
    
    public Ship(IWeapon primaryWeapon, IWeapon secondaryWeapon)
    {
        _primaryWeapon = primaryWeapon;
        _secondaryWeapon = secondaryWeapon;
        _health = StartHealth;
    }

    public void ApplyDamage()
    {
        _health -= MaxRemoveHealth;

        if (_health <= 0)
        {
            _health = 0;
            OnDeathEvent?.Invoke();
        }
        
        OnHealthChangedEvent?.Invoke(_health);
    }

    public void Update()
    {
        _primaryWeapon.Update();
        _secondaryWeapon.Update();
    }


    public void OnTriggerEnter(ICustomCollider other)
    {
        Debug.Log("Ship trigger");
    }

    public void OnCollisionEnter(ICustomCollider other)
    {
        Debug.Log("Ship collision");
    }
}
