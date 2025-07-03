using System;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Core.Entity.Weapons
{
    public class LaserWeapon : StandardWeapon
    {
        private const int RefillAmmo = 1;
    
        private readonly float _refillTime;
    
        private int _ammo;
    
        private float _refillTimer;

        public int CurrentAmmo
        {
            get { return _ammo; }
            
            private set
            {
                _ammo = value;
                OnChangedAmmoEvemt?.Invoke(_ammo);
            }
        }
        
        public event Action OnShootEvent;

        public event Action<int> OnChangedAmmoEvemt;

        public LaserWeapon(int startAmmo, float cooldownTime, float refillTime,
            IProjectilePool projectilePool, ProjectileType projectileType)
            : base(cooldownTime, projectilePool, projectileType)
        {
            _ammo = startAmmo;
            _shootTimer = 0;
            _refillTime = refillTime;
            _refillTimer = _refillTime;
        }
    
        public override void TryShoot(Vector2 position, Quaternion rotation, float speedParent = 0)
        {
            if (_isCooldownOver == true && CurrentAmmo > 0)
            {
                CurrentAmmo--;
                OnShootEvent?.Invoke();
                _shootTimer = _cooldownTime; 
                _isCooldownOver = false;
                _projectilePool.Get(_projectileType, position, rotation).Lunch(speedParent);
            }
            
        }

        public override void Update()
        {
            base.Update();
            UpdateRefillTimer();
        }

        private void AddAmmo(int count)
        {
            CurrentAmmo += count;
        }
    
        private void UpdateRefillTimer()
        {
            _refillTimer -= Time.deltaTime;

            if (_refillTimer <= 0)
            {
                AddAmmo(RefillAmmo);
                _refillTimer = _refillTime;
            }
        }

    }
}
