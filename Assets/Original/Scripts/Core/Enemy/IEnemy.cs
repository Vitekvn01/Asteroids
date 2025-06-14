using System;
using Original.Scripts.Core.Interfaces;

namespace Original.Scripts.Core.Enemy
{
    public interface IEnemy : IActivatable
    {
        public void SetSpeed(float speed);

        public event Action<IEnemy> OnEnemyDeath;

        public void Death();
    }
}