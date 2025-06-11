using System;
using Zenject;

public class ShipController : ITickable, IDisposable
{
    private readonly IShipView _shipView;
    private readonly Ship _ship;
    private readonly ShipMovement _shipMovement;

    private readonly IInput _input;

    private bool _isAlive = false;
    
    public ShipController(IInput input, Ship ship, IShipView view, ShipMovement shipMovement)
    {
        _input = input;
        
        _shipView = view;
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
        
            _shipMovement.Move(axisY, -axisX);
        
            _ship.Update();

            if (_input.CheckPressedFireSecondary())
            {
                _ship.PrimaryWeapon.TryShoot(_shipView.ShootPoint.position,_shipView.ShootPoint.rotation );
            }

            if (_input.CheckPressedFirePrimary())
            {
                _ship.SecondaryWeapon.TryShoot(_shipView.ShootPoint.position,_shipView.ShootPoint.rotation );
            }
        }
    }

    private void OnDeathEvent()
    {
        _isAlive = false;
        _shipView.Death();
    }
    
    public void Dispose()
    {
        _ship.OnDeathEvent -= OnDeathEvent;
    }
}
