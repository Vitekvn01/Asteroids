using System;
using UnityEngine;
using Zenject;

public class StandardWeapon : IWeapon, ITickable
{
    protected readonly float _cooldownTime;
    
    protected float _shootTimer;
    
    protected bool _isCooldownOver;

    public float ShootTimer => _shootTimer;

    public event Action OnShootEvent;

    public StandardWeapon(float cooldownTime)
    {
        _shootTimer = 0;
    }
    public virtual bool TryShoot()
    {
        bool isCanShoot = false;

        if (_isCooldownOver == true)
        {
            OnShootEvent?.Invoke();
            _shootTimer = _cooldownTime; 
            isCanShoot = true;
            Debug.Log("Shoot:" );
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
        else
        {
            _isCooldownOver = false;
        }
    }

    public virtual void Tick()
    {
        UpdateShootTimer();
    }
}
