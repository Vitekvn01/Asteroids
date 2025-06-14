using UnityEngine;

namespace Original.Scripts.Core.Weapons
{
   public interface IWeapon
   {
      public bool TryShoot(Vector2 position, Quaternion rotation);

      public void Update();
   }
}
