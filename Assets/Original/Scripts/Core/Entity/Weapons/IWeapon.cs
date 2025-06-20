using Original.Scripts.Core.Entity.Projectiles;
using UnityEngine;

namespace Original.Scripts.Core.Entity.Weapons
{
   public interface IWeapon
   {
      public ProjectileType ProjectileType { get; }
      public bool TryShoot(Vector2 position, Quaternion rotation, float speedParent);

      public void Update();
   }
}
