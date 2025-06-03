using System;
using Zenject;

public class Ship
{
    private const int StartHealth = 3;
    private const int MaxRemoveHealth = 1;
    
    private int _health;

    private IWeapon _laserWeapon;
    private IWeapon _standardWeapon;

    public IWeapon LaserWeapon => _laserWeapon;
    public IWeapon BulletWeapon => _standardWeapon;

    public event Action<int> OnHealthChangedEvent;
    public event Action OnDeathEvent;
    
    [Inject]
    public Ship([Inject(Id = "LaserWeapon")] IWeapon laserWeapon, [Inject(Id = "StandardWeapon")] IWeapon standardWeapon)
    {
        _laserWeapon = laserWeapon;
        _standardWeapon = standardWeapon;
        _health = StartHealth;
    }

    public void ApplyDamage()
    {
        _health -= MaxRemoveHealth;

        if (_health <= 0)
        {
            _health = 0;
            OnDeathEvent?.Invoke();
        }
        
        OnHealthChangedEvent?.Invoke(_health);
    }
}
