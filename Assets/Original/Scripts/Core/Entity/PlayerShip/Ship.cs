using System;
using Cysharp.Threading.Tasks;
using Original.Scripts.Core.Config;
using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using UnityEngine;

namespace Original.Scripts.Core.Entity.PlayerShip
{
    public class Ship : IColliderHandler, IActivatable
    {
        private const int MaxRemoveHealth = 1;
        
        private readonly CustomPhysics _physics;
        private readonly IShipView _shipView;

        private readonly int MaxHealth;
        
        private int _health;
        
        private float _moveSpeed;
        private float _rotationSpeed;
        private float _maxSpeed;

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
        
        public Ship(IWeapon primaryWeapon, IWeapon secondaryWeapon, CustomPhysics physics, IShipView shipView, 
            PlayerConfig playerConfig)
        {
            _primaryWeapon = primaryWeapon;
            _secondaryWeapon = secondaryWeapon;
            
            MaxHealth = playerConfig.Health;
            _health = playerConfig.Health;
            _moveSpeed = playerConfig.MoveSpeed;
            _rotationSpeed = playerConfig.RotationSpeed;
            _maxSpeed = playerConfig.MaxSpeed;
            _isActive = true;

            _physics = physics;
            _shipView = shipView;
        }

        public void ApplyDamage()
        {
            _health -= MaxRemoveHealth;

            if (_health <= 0)
            {
                _health = 0;
                OnDeathEvent?.Invoke();
                Deactivate();
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
  
            _shipView.Transform.position = pos;
            _shipView.Transform.rotation = rotation;
            _physics.SetPosition(pos);
            _physics.SetRotation(rotation.z);
            ResetHealth();
            _shipView.SetActive(true);
            _isActive = true;
        }


        public void Deactivate()
        {
            _isActive = false;
            _shipView.SetActive(false);
        }
        
        private void ResetHealth()
        {
            _health = MaxHealth;
            OnChangedHealthEvent?.Invoke(_health);
        }

        private async UniTaskVoid BecomeInvincibleAsync(float duration)
        {
            _isInvincible = true;
            _canControl = false;
            
            _physics.Collider.SetLayer(0);
            _physics.Collider.SetCollisionMask(0);

            await UniTask.Delay(TimeSpan.FromSeconds(duration));
            
            _isInvincible = false;
            _canControl = true;
            
            _physics.Collider.SetLayer(PhysicsLayer.Player);
            _physics.Collider.SetCollisionMask(PhysicsLayer.Enemy);
        }
        

    }
}
