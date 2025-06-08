using System;
using UnityEngine;
using Zenject;

public class WeaponFactory : IWeaponFactory
{
    private readonly IObjectPool<Projectile> _projectilePool;
    
    [Inject]
    public WeaponFactory(IObjectPool<Projectile> projectilePool)
    {
        _projectilePool = projectilePool;
    }
    
    public IWeapon Create(WeaponType weaponType)
    {
        IWeapon created;
        
        switch (weaponType)
        {
            case WeaponType.StandardWeapon:
                created = new StandardWeapon(WeaponSettings.StandardCooldown, _projectilePool);
                break;
            case WeaponType.LaserWeapon:
                created = new LaserWeapon(WeaponSettings.LaserAmmo, WeaponSettings.LaserCooldown, WeaponSettings.LaserRefillTime, _projectilePool);
                break;
            default:
                throw new ArgumentException("Unknown weapon type", nameof(weaponType));
        }
        
        return created;
    }
}