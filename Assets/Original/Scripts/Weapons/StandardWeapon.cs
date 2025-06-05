using System;
using UnityEngine;

public class StandardWeapon : IWeapon
{
    protected readonly float _cooldownTime;
    
    protected float _shootTimer;
    
    protected bool _isCooldownOver;

    protected IProjectileFactory _projectileFactory;

    protected Transform _shootPoint;

    public float ShootTimer => _shootTimer;

    public event Action OnShootEvent;

    public StandardWeapon(float cooldownTime, Transform shootPoint, IProjectileFactory projectileFactory)
    {
        _cooldownTime = cooldownTime;
        _shootPoint = shootPoint;
        _projectileFactory = projectileFactory;
        
        _shootTimer = 0;
        _isCooldownOver = true;
    }
    public virtual bool TryShoot()
    {
        bool isCanShoot = false;

        if (_isCooldownOver == true)
        {
            OnShootEvent?.Invoke();
            _shootTimer = _cooldownTime;
            isCanShoot = true;
            Debug.Log("ShootStandartWeapon" );
            _isCooldownOver = false;
            _projectileFactory.Create(_shootPoint.position, _shootPoint.rotation);
        }

        return isCanShoot;
    }
    
    protected virtual void UpdateShootTimer()
    {
        _shootTimer -= Time.deltaTime;

        if (_shootTimer <= 0)
        {
            _isCooldownOver = true;
        }
    }

    public virtual void Update()
    {
        UpdateShootTimer();
    }
}
