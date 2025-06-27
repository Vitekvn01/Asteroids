using Original.Scripts.Core.Entity.PlayerShip;

namespace Original.Scripts.Core.Signals
{
    public class PlayerCreateSignal
    {
        public ShipController ShipController;

        public PlayerCreateSignal(ShipController shipController)
        {
            ShipController = shipController;
        }
    }
}