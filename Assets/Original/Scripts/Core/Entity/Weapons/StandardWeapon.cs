using System;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.Weapons
{
    public class StandardWeapon : IWeapon
    {
        protected readonly ProjectileType _projectileType;
        
        protected readonly float _cooldownTime;
    
        protected float _shootTimer;
    
        protected bool _isCooldownOver;

        protected IProjectilePool _projectilePool;
        
        public float ShootTimer => _shootTimer;
        
        [Inject]
        public StandardWeapon(float cooldownTime, IProjectilePool projectilePool, ProjectileType projectileType)
        {
            _cooldownTime = cooldownTime;
            _projectilePool = projectilePool;
            _projectileType = projectileType;

            _shootTimer = 0;
            _isCooldownOver = true;
        }
        
        public virtual void TryShoot(Vector2 position, Quaternion rotation, float speedParent = 0)
        {

            if (_isCooldownOver)
            {
                _shootTimer = _cooldownTime;
                _isCooldownOver = false;
                _projectilePool.Get(_projectileType, position, rotation).Lunch(speedParent);
            }
        }
    
        protected void UpdateShootTimer()
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
