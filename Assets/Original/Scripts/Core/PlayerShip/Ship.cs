using System;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.Weapons;
using UnityEngine;

namespace Original.Scripts.Core.PlayerShip
{
    public class Ship : IColliderHandler, IActivatable
    {
        private const int MaxRemoveHealth = 1;
        
        private readonly CustomPhysics _physics;
        
        private int _health;
        
        private float _moveSpeed;
        private float _rotationSpeed;
        private float _maxSpeed = 100;
        
        private float _timerTest;

        private bool _isActive;
    
        private IWeapon _primaryWeapon;
        private IWeapon _secondaryWeapon;

        public CustomPhysics Physics => Physics;
    
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;

        public float MaxSpeed => _maxSpeed;

        public bool IsActive => _isActive;
    
        public IWeapon PrimaryWeapon => _primaryWeapon;
        public IWeapon SecondaryWeapon => _secondaryWeapon;
    

        public event Action<int> OnHealthChangedEvent;
        public event Action OnDeathEvent;
    
    
        public Ship(IWeapon primaryWeapon, IWeapon secondaryWeapon, int health, float moveSpeed, float rotationSpeed, float maxSpeed)
        {
            _primaryWeapon = primaryWeapon;
            _secondaryWeapon = secondaryWeapon;

            _health = health;
            _moveSpeed = moveSpeed;
            _rotationSpeed = rotationSpeed;
            _maxSpeed = maxSpeed;
            _isActive = true;
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


        public void Activate(Vector3 pos, Quaternion rotation)
        {
            
        }

        public void Deactivate()
        {
            
        }
    }
}
