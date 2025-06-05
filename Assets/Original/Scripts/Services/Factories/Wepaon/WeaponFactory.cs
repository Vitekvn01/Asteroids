using System;
using UnityEngine;
using Zenject;

public class WeaponFactory : IWeaponFactory
{
    private readonly IProjectileFactory _projectileFactory;
    
    [Inject]
    public WeaponFactory(IProjectileFactory projectileFactory)
    {
        _projectileFactory = projectileFactory;
    }
    
    public IWeapon Create(WeaponType weaponType, Transform shootPoint)
    {
        IWeapon created;
        
        switch (weaponType)
        {
            case WeaponType.StandardWeapon:
                created = new StandardWeapon(WeaponSettings.StandardCooldown, shootPoint, _projectileFactory);
                break;
            case WeaponType.LaserWeapon:
                created = new LaserWeapon(WeaponSettings.LaserAmmo, WeaponSettings.LaserCooldown, WeaponSettings.LaserRefillTime, shootPoint, _projectileFactory);
                break;
            default:
                throw new ArgumentException("Unknown weapon type", nameof(weaponType));
        }
        
        return created;
    }
}