using Original.Scripts.Core.Entity.PlayerShip;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IUIFactory
    {
        public void CreateShipHud(ShipController shipController);

        public void CreateScoreHud();
    }
}