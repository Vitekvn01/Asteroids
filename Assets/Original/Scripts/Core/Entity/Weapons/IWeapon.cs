using UnityEngine;

namespace Original.Scripts.Core.Entity.Weapons
{
   public interface IWeapon
   {
      public bool TryShoot(Vector2 position, Quaternion rotation, float speedParent);

      public void Update();
   }
}
