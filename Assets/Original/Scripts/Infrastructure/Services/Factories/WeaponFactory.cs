using System;
using Original.Scripts.Core;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Weapons;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly IObjectPool<Projectile> _projectilePool;
        private readonly IConfigProvider _configLoader;
    
        [Inject]
        public WeaponFactory(IObjectPool<Projectile> projectilePool, IConfigProvider configLoader)
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
                    created = new StandardWeapon(_configLoader.WeaponConfig.StandardCooldown, _projectilePool);
                    break;
                case WeaponType.LaserWeapon:
                    created = new LaserWeapon(_configLoader.WeaponConfig.LaserAmmo, _configLoader.WeaponConfig.LaserCooldown, _configLoader.WeaponConfig.LaserRefillTime, _projectilePool);
                    break;
                default:
                    throw new ArgumentException("Unknown weapon type", nameof(weaponType));
            }
        
            return created;
        }
    }
}