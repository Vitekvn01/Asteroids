using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.PlayerShip;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Application.Gameplay.Spawner
{
   public class PlayerSpawner
   {
      private readonly Vector2 _spawnPos;
      private readonly float _angle;
      private readonly IShipFactory _shipFactory;
      private Ship _ship;

      public Ship Ship => _ship;

      [Inject]
      public PlayerSpawner(IShipFactory shipFactory)
      {
         _shipFactory = shipFactory;
         _spawnPos = new Vector2(0, 0);
         _ship = _shipFactory.Create(_spawnPos).Ship;
         _ship.Deactivate();
         Spawn();
      }

      public void Spawn()
      {
         _ship.Activate(_spawnPos, Quaternion.Euler(0,0,_angle));
      }
   }
}
