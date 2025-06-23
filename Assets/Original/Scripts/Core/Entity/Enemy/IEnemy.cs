using System;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Physics;

namespace Original.Scripts.Core.Entity.Enemy
{
    public interface IEnemy : IActivatable 
    {   
        public EnemyType Type { get; }
        public CustomPhysics Physics { get; } 
        public event Action<IEnemy> OnEnemyDeath;
        
        public void SetSpeed(float speed);

        public void Death();
    }
}