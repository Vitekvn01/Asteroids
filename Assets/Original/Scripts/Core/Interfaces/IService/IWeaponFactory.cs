using Original.Scripts.Core.Weapons;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IWeaponFactory
    {
        public IWeapon Create(WeaponType weaponType);
    }
}