using System;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Weapons
{
    public class StandardWeapon : IWeapon
    {
        protected readonly float _cooldownTime;
    
        protected float _shootTimer;
    
        protected bool _isCooldownOver;

        protected IObjectPool<Projectile> _projectilePool;
    

        public float ShootTimer => _shootTimer;

        public event Action OnShootEvent;
    
        [Inject]
        public StandardWeapon(float cooldownTime, IObjectPool<Projectile> projectilePool)
        {
            _cooldownTime = cooldownTime;
            _projectilePool = projectilePool;
        
            _shootTimer = 0;
            _isCooldownOver = true;
        }
    

        public virtual bool TryShoot(Vector2 position, Quaternion rotation)
        {
            bool isCanShoot = false;

            if (_isCooldownOver)
            {
                OnShootEvent?.Invoke();
                _shootTimer = _cooldownTime;
                isCanShoot = true;
                Debug.Log("ShootStandartWeapon" );
                _isCooldownOver = false;
                _projectilePool.Get(position, rotation);
            }

            return isCanShoot;
        }
    
        protected virtual void UpdateShootTimer()
        {
            _shootTimer -= Time.deltaTime;

            if (_shootTimer <= 0)
            {
                _isCooldownOver = true;
            }
        }

        public virtual void Update()
        {
            UpdateShootTimer();
        }
    }
}
