using System;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.Weapons
{
    public class StandardWeapon : IWeapon
    {
        protected readonly float _cooldownTime;
    
        protected float _shootTimer;
    
        protected bool _isCooldownOver;

        protected IProjectilePool _projectilePool;
        
        public float ShootTimer => _shootTimer;
        
        public ProjectileType ProjectileType { get; }

        public event Action OnShootEvent;
    
        [Inject]
        public StandardWeapon(float cooldownTime, IProjectilePool projectilePool, ProjectileType projectileType)
        {
            _cooldownTime = cooldownTime;
            _projectilePool = projectilePool;
            ProjectileType = projectileType;

            _shootTimer = 0;
            _isCooldownOver = true;
        }
        
        public virtual bool TryShoot(Vector2 position, Quaternion rotation, float speedParent = 0)
        {
            bool isCanShoot = false;

            if (_isCooldownOver)
            {
                OnShootEvent?.Invoke();
                _shootTimer = _cooldownTime;
                isCanShoot = true;
                Debug.Log("ShootStandartWeapon" );
                _isCooldownOver = false;
                _projectilePool.Get(ProjectileType, position, rotation).Lunch(speedParent);
            }

            return isCanShoot;
        }
    
        protected virtual void UpdateShootTimer()
        {
            if (_shootTimer <= 0)
            {
                _isCooldownOver = true;
            }
            else
            {
                _shootTimer -= Time.deltaTime;
            }
        }

        public virtual void Update()
        {
            UpdateShootTimer();
        }
    }
}
