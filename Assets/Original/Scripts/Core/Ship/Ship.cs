using System;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.Weapons;
using UnityEngine;

namespace Original.Scripts.Core.Ship
{
    public class Ship : IColliderHandler
    {
        private const int StartHealth = 3;
        private const int MaxRemoveHealth = 1;
        
        private int _health;
        
        private float _moveSpeed = 10;
        private float _rotationSpeed = 100;
        
        private float _timerTest;
    
        private IWeapon _primaryWeapon;
        private IWeapon _secondaryWeapon;
    
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
    
        public IWeapon PrimaryWeapon => _primaryWeapon;
        public IWeapon SecondaryWeapon => _secondaryWeapon;
    

        public event Action<int> OnHealthChangedEvent;
        public event Action OnDeathEvent;
    
    
        public Ship(IWeapon primaryWeapon, IWeapon secondaryWeapon, int health, float moveSpeed, float rotationSpeed)
        {
            _primaryWeapon = primaryWeapon;
            _secondaryWeapon = secondaryWeapon;

            _health = health;
            _moveSpeed = moveSpeed;
            _rotationSpeed = rotationSpeed;
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
}
