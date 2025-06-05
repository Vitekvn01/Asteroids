using UnityEngine;

public interface IShipFactory
{
   public ShipController Create(Vector2 pos, float rot = 0);
}
