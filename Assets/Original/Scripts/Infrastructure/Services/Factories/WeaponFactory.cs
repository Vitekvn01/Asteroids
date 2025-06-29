using System;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Interfaces.IService;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly IProjectilePool _projectilePool;
        private readonly IConfigProvider _configLoader;
    
        [Inject]
        public WeaponFactory(IProjectilePool projectilePool, IConfigProvider configLoader)
        {
            _projectilePool = projectilePool;
            _configLoader = configLoader;
        }
    
        public IWeapon Create(WeaponType weaponType)
        {
            IWeapon created;
        
            switch (weaponType)
            {
                case WeaponType.StandardWeapon:
                    created = new StandardWeapon(_configLoader.WeaponConfig.BulletWeaponCooldown, _projectilePool, ProjectileType.Bullet);
                    break;
                case WeaponType.LaserWeapon:
                    created = new LaserWeapon(_configLoader.WeaponConfig.LaserAmmo, _configLoader.WeaponConfig.LaserWeaponCooldown, _configLoader.WeaponConfig.LaserRefillTime, _projectilePool, ProjectileType.Laser);
                    break;
                case WeaponType.EnemyWeapon:
                    created = new StandardWeapon(_configLoader.WeaponConfig.EnemyFireCooldown, _projectilePool, ProjectileType.EnemyBullet);
                    break;
                default:
                    throw new ArgumentException("Unknown weapon type", nameof(weaponType));
            }
        
            return created;
        }
    }
}