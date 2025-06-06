using UnityEngine;
using Zenject;

public class PlayerSpawner
{
   private readonly IShipFactory _shipFactory;

   [Inject]
   public PlayerSpawner(IShipFactory shipFactory)
   {
      _shipFactory = shipFactory;
      Spawn(new Vector2(0, 100), 180);
      Spawn(new Vector2(0, 0), 0);
   }

   public void Spawn(Vector2 pos, float rot = 0)
   {
      _shipFactory.Create(pos, rot);
   }
}
