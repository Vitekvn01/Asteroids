using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Application.Gameplay.Spawner
{
   public class PlayerSpawner
   {
      private readonly IShipFactory _shipFactory;
      private readonly IUIFactory _uiFactory;
      
      private readonly float _angle;

      private Vector2 _spawnPos;
      
      private ShipController _shipController;

      public ShipController ShipController => _shipController;

      [Inject]
      public PlayerSpawner(IShipFactory shipFactory)
      {
         _shipFactory = shipFactory;

         
         _spawnPos = new Vector2(0, 0);
         _shipController = _shipFactory.Create(_spawnPos);
         _shipController.Ship.Deactivate();
      }
      public void Spawn()
      {
         _shipController.Ship.Activate(_spawnPos, Quaternion.Euler(0,0,_angle));
      }


   }
}
