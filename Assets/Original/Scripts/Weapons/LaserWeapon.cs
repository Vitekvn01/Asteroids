using System;
using UnityEngine;

public class LaserWeapon : StandardWeapon
{
    private const int RefillAmmo = 1;
    
    private readonly float _refillTime;
    
    private int _ammo;
    
    private float _refillTimer;
    
    public event Action OnShootEvent;

    public LaserWeapon(int startAmmo, float cooldownTime, float refillTime, Transform shootPoint,
        IProjectileFactory projectileFactory) : base(cooldownTime, shootPoint, projectileFactory)
    {
        _ammo = startAmmo;
        _shootTimer = 0;
        _refillTime = refillTime;
        _refillTimer = _refillTime;
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
            Debug.Log("ShootLaserWeapon");
            _isCooldownOver = false;
        }

        return isCanShoot;
    }

    public override void Update()
    {
        base.Update();
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
            _refillTimer = _refillTime;
        }
    }

}
