using System;
using Cysharp.Threading.Tasks;
using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Physics;
using UnityEngine;

namespace Original.Scripts.Core.Entity.PlayerShip
{
    public class Ship : IColliderHandler, IActivatable
    {
        private const int MaxRemoveHealth = 1;
        
        private readonly CustomPhysics _physics;
        
        private int _health;
        
        private float _moveSpeed;
        private float _rotationSpeed;
        private float _maxSpeed = 100;

        private bool _isActive;
        private bool _isInvincible;

        private bool _canControl = true;
    
        private IWeapon _primaryWeapon;
        private IWeapon _secondaryWeapon;

        public CustomPhysics Physics => _physics;

        public int Health => _health;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;

        public float MaxSpeed => _maxSpeed;

        public bool IsActive => _isActive;

        public bool CanControl => _canControl;
    
        public IWeapon PrimaryWeapon => _primaryWeapon;
        public IWeapon SecondaryWeapon => _secondaryWeapon;
    

        public event Action<int> OnChangedHealthEvent;
        public event Action OnDeathEvent;
        
        public Ship(IWeapon primaryWeapon, IWeapon secondaryWeapon, int health, float moveSpeed, float rotationSpeed, float maxSpeed, CustomPhysics physics)
        {
            _primaryWeapon = primaryWeapon;
            _secondaryWeapon = secondaryWeapon;

            _health = health;
            _moveSpeed = moveSpeed;
            _rotationSpeed = rotationSpeed;
            _maxSpeed = maxSpeed;
            _isActive = true;

            _physics = physics;
        }

        public void ApplyDamage()
        {
            _health -= MaxRemoveHealth;

            if (_health <= 0)
            {
                _health = 0;
                OnDeathEvent?.Invoke();
            }
        
            OnChangedHealthEvent?.Invoke(_health);
        }

        public void Update()
        {
            if (_canControl)
            {
                _primaryWeapon.Update();
                _secondaryWeapon.Update();
            }
        }
        


        public void OnTriggerEnter(ICustomCollider other)
        {

        }

        public void OnCollisionEnter(ICustomCollider other)
        {
             if (other.Handler is IEnemy)
             {
                 ApplyDamage();
                 BecomeInvincibleAsync(3f).Forget();
             }
        }


        public void Activate(Vector3 pos, Quaternion rotation)
        {
            
        }

        public void Deactivate()
        {
            
        }

        private async UniTaskVoid BecomeInvincibleAsync(float duration)
        {
            
            Debug.Log("BecomeInvincibleAsync start");
            _isInvincible = true;
            _canControl = false;
            
            _physics.Collider.SetLayer(0);
            _physics.Collider.SetCollisionMask(0);

            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            
            Debug.Log("BecomeInvincibleAsync finish");
            _isInvincible = false;
            _canControl = true;
            
            _physics.Collider.SetLayer(PhysicsLayer.Player);
            _physics.Collider.SetCollisionMask(PhysicsLayer.Enemy);
        }
        

    }
}
