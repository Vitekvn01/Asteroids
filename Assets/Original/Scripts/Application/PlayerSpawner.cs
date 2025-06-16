using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.PlayerShip;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core
{
   public class PlayerSpawner
   {
      private readonly IShipFactory _shipFactory;
      private Ship _ship;

      public Ship Ship => _ship;

      [Inject]
      public PlayerSpawner(IShipFactory shipFactory)
      {
         _shipFactory = shipFactory;
         Spawn(new Vector2(0, 0), 100);
      }

      public void Spawn(Vector2 pos, float rot = 0)
      {
         _ship = _shipFactory.Create(pos, rot).Ship;
      }
   }
}
