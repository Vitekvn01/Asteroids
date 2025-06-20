using Original.Scripts.Core.Entity.PlayerShip;
using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IService
{
   public interface IShipFactory
   {
      public ShipController Create(Vector2 pos, float rot = 0);
   }
}
