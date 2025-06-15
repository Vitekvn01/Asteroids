using System;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Core.Weapons
{
    public class LaserWeapon : StandardWeapon
    {
        private const int RefillAmmo = 1;
    
        private readonly float _refillTime;
    
        private int _ammo;
    
        private float _refillTimer;
    
        public event Action OnShootEvent;

        public LaserWeapon(int startAmmo, float cooldownTime, float refillTime,
            IObjectPool<Projectile> projectilePool) : base(cooldownTime, projectilePool)
        {
            _ammo = startAmmo;
            _shootTimer = 0;
            _refillTime = refillTime;
            _refillTimer = _refillTime;
        }
    
        public override bool TryShoot(Vector2 position, Quaternion rotation, float parentSpeed = 0)
        {
            bool isCanShoot = false;

            if (_isCooldownOver == true && _ammo > 0)
            {
                _ammo--;
                OnShootEvent?.Invoke();
                _shootTimer = _cooldownTime; 
                isCanShoot = true;
                Debug.Log("ShootLaserWeapon");
                _isCooldownOver = false;
            }

            return isCanShoot;
        }

        public override void Update()
        {
            base.Update();
            UpdateRefillTimer();
        }

        private void AddAmmo(int count)
        {
            _ammo += count;
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
