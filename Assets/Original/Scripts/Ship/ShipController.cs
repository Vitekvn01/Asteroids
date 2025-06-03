using Zenject;

public class ShipController : ITickable
{
    private const string LaserFireButton = "Fire1";
    private const string BulletFireButton = "Fire2";

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

        if (_input.CheckGetButtonDown(LaserFireButton))
        {
            _ship.LaserWeapon.TryShoot();
        }

        if (_input.CheckGetButtonDown(BulletFireButton))
        {
            _ship.BulletWeapon.TryShoot();
        }
    }
}
