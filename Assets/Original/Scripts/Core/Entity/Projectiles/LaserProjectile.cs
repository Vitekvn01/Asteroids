using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;

namespace Original.Scripts.Core.Entity.Projectiles
{
    public class LaserProjectile : Projectile
    {
        public LaserProjectile(IProjectileView view, CustomPhysics physics, float speed, float lifetime,
            ProjectileType type) : base(view, physics, speed, lifetime, type)
        {
        }

        public override void OnTriggerEnter(ICustomCollider other)
        {
            if (other.Handler is IEnemy enemy)
            {
                enemy.Death();
            }
        }
    }
}