using System;
using Zenject;

public class ShipController : ITickable, IDisposable
{
    private readonly ShipBehaviour _shipBehaviour;
    private readonly Ship _ship;
    private readonly ShipMovement _shipMovement;

    private readonly IInput _input;

    private bool _isAlive = false;
    
    [Inject]
    public ShipController(IInput input, Ship ship, ShipBehaviour behaviour, ShipMovement shipMovement)
    {
        _input = input;
        
        _shipBehaviour = behaviour;
        _ship = ship;
        _shipMovement = shipMovement;

        _isAlive = true;
        
        _ship.OnDeathEvent += OnDeathEvent;
    }
    public void Tick()
    {
        if (_isAlive)
        {
            float axisX = _input.GetAxisX();
            float axisY = _input.GetAxisY();
        
            _shipMovement.Move(axisY, axisX);
        
            _ship.Update();

            if (_input.CheckPressedFireSecondary())
            {
                _ship.PrimaryWeapon.TryShoot();
            }

            if (_input.CheckPressedFirePrimary())
            {
                _ship.SecondaryWeapon.TryShoot();
            }
        }
    }

    private void OnDeathEvent()
    {
        _isAlive = false;
        _shipBehaviour.Death();
    }
    
    public void Dispose()
    {
        _ship.OnDeathEvent -= OnDeathEvent;
    }
}
