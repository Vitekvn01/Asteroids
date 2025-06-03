using System;
using UnityEngine;

public class LaserWeapon : StandardWeapon
{
    private const int RefillAmmo = 1;
    
    private const float RefillTime = 10f;
    
    private int _ammo;
    
    private float _refillTimer;
    
    public event Action OnShootEvent;

    public LaserWeapon(int startAmmo, float cooldownTime) : base(cooldownTime)
    {
        _ammo = startAmmo;
        _shootTimer = 0;
        _refillTimer = RefillTime;
    }
    
    public override bool TryShoot()
    {
        bool isCanShoot = false;

        if (_isCooldownOver == true && _ammo > 0)
        {
            _ammo--;
            OnShootEvent?.Invoke();
            _shootTimer = _cooldownTime; 
            isCanShoot = true;
        }

        return isCanShoot;
    }

    public override void Tick()
    {
        base.Tick();
        UpdateRefillTimer();
    }

    public void AddAmmo(int count)
    {
        _ammo += count;
    }
    
    private void UpdateRefillTimer()
    {
        _refillTimer -= Time.deltaTime;

        if (_refillTimer <= 0)
        {
            AddAmmo(RefillAmmo);
            _refillTimer = RefillTime;
        }
    }

}
