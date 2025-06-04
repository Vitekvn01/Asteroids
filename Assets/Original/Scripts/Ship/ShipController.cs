using Zenject;

public class ShipController : ITickable
{
    private ShipBehaviour _shipBehaviour;
    private Ship _ship;
    private ShipMovement _shipMovement;

    private IInput _input;
    
    [Inject]
    public ShipController(IInput input, Ship ship, ShipBehaviour behaviour, ShipMovement shipMovement)
    {
        _input = input;
        
        _shipBehaviour = behaviour;
        _ship = ship;
        _shipMovement = shipMovement;
    }
    public void Tick()
    {
        float axisX = _input.GetAxisX();
        float axisY = _input.GetAxisY();
        
        _shipMovement.Move(axisY, axisX);

        if (_input.CheckPressedFireSecondary())
        {
            _ship.LaserWeapon.TryShoot();
        }

        if (_input.CheckPressedFirePrimary())
        {
            _ship.BulletWeapon.TryShoot();
        }
    }
}
