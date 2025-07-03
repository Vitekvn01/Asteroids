using System;
using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Signals;
using UniRx;
using UnityEngine;
using Zenject;

public class ShipHUDViewModel : ITickable, IDisposable
{
    private readonly SignalBus _signalBus;
    
    private readonly Ship _ship;
    private readonly ShipMovement _movement;
    private readonly LaserWeapon _laserWeapon;
    
    private readonly Subject<Unit> _onStartPlay = new();
    private readonly Subject<Unit> _onPlayerDead = new();
    public IObservable<Unit> OnStartPlay => _onStartPlay;
    public IObservable<Unit> PlayerDead => _onPlayerDead;
    public ReactiveProperty<int> Health { get; } = new();
    public ReactiveProperty<int> LaserAmmo { get; } = new();
    public ReactiveProperty<float> Rotation { get; } = new();
    public ReactiveProperty<float> Speed { get; } = new();
    public ReactiveProperty<float> LaserCooldown { get; } = new();
    
    public ReactiveProperty<Vector2> Position { get; } = new();

    private readonly CompositeDisposable _disposables = new();

    public ShipHUDViewModel(SignalBus signalBus, ShipController shipController)
    {
        _signalBus = signalBus;
        _ship = shipController.Ship;
        _movement = shipController.ShipMovement;
        
        _laserWeapon = _ship.SecondaryWeapon as LaserWeapon;
        
        if (_laserWeapon != null)
        {
            LaserAmmo.Value = _laserWeapon.CurrentAmmo;
            _laserWeapon.OnChangedAmmoEvemt += OnChangedAmmo;
        }
        
        Health.Value = _ship.Health;
        _ship.OnChangedHealthEvent += OnChangedHealth;
        
        _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
        _signalBus.Subscribe<StartGameSignal>(OnStartGame);
    }

    public void Tick()
    {
        if (_ship.IsActive)
        {
            Position.Value = _movement.Physics.Position;
            Rotation.Value = NormalizeAngle(_movement.Physics.Rotation);
            Speed.Value = _movement.Physics.Velocity.magnitude;

            if (_laserWeapon != null)
            {
                LaserCooldown.Value = _laserWeapon.ShootTimer;
            }
        }
    }
    
    public void Dispose()
    {
        _disposables.Dispose();
        _ship.OnChangedHealthEvent -= OnChangedHealth;
        _laserWeapon.OnChangedAmmoEvemt -= OnChangedAmmo;
    }
    
    private float NormalizeAngle(float angle)
    {
        angle %= 360f;

        if (angle < 0)
        {
            angle += 360f;
        }
        
        return angle;
    }
    
    private void OnChangedHealth(int newHealth)
    {
        Health.Value = newHealth;
    }
    
    private void OnChangedAmmo(int newAmmo)
    {
        LaserAmmo.Value = newAmmo;
    }
    
    private void OnStartGame(StartGameSignal signal)
    {
        _onStartPlay.OnNext(Unit.Default);
    }
    
    private void OnPlayerDead(PlayerDeadSignal signal)
    {
        _onPlayerDead.OnNext(Unit.Default);
    }
}