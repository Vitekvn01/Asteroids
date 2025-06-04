using System;
using UnityEngine;
using Zenject;

public class WeaponFactory : IWeaponFactory
{
    private readonly TickableManager _tickableManager;
    private readonly IProjectileFactory _projectileFactory;
    
    [Inject]
    public WeaponFactory(TickableManager tickableManager, IProjectileFactory projectileFactory)
    {
        _tickableManager = tickableManager;
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
        
        _tickableManager.Add((StandardWeapon)created);
        return created;
    }
}