using System;
using UnityEngine;
using Zenject;

public class Ship
{
    private const int StartHealth = 3;
    private const int MaxRemoveHealth = 1;
    
    private int _health;

    private IWeapon _primaryWeapon;
    private IWeapon _secondaryWeapon;
    
    private float _timerTest;
    
    public IWeapon PrimaryWeapon => _primaryWeapon;
    public IWeapon SecondaryWeapon => _secondaryWeapon;

    public event Action<int> OnHealthChangedEvent;
    public event Action OnDeathEvent;
    
    [Inject]
    public Ship(ShipBehaviour behaviour, IWeapon primaryWeapon,  IWeapon secondaryWeapon)
    {
        _primaryWeapon = primaryWeapon;
        _secondaryWeapon = secondaryWeapon;
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

    public void Update()
    {
        _primaryWeapon.Update();
        _secondaryWeapon.Update();

        /*_timerTest += Time.deltaTime;

        if (_timerTest >= 5)
        {
            OnDeathEvent?.Invoke();
        }*/
    }
}
