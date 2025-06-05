using UnityEngine;

public interface IWeaponFactory
{
    public IWeapon Create(WeaponType weaponType, Transform shootPoint);
}