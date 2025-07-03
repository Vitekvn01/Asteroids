using System;

namespace Original.Scripts.Core.Config
{
    [Serializable]
    public class WeaponConfig
    {
        public float BulletLifetime;
        public float BulletWeaponCooldown;
        public float BulletProjectileSpeed;

        public float LaserLifetime;
        public int LaserAmmo;
        public float LaserWeaponCooldown;
        public float LaserRefillTime;
        public float LaserProjectileSpeed;

        public float EnemyBulletLifetime;
        public float EnemyFireCooldown;
        public float EnemyProjectileSpeed;
    }
}
