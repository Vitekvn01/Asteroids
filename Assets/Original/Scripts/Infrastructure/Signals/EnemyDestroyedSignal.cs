using Original.Scripts.Core.Enemy;

namespace Original.Scripts.Infrastructure.Signals
{
    public class EnemyDestroyedSignal
    {
        public EnemyType EnemyType { get; }

        public EnemyDestroyedSignal(EnemyType enemyType)
        {
            EnemyType = enemyType;
        }
    }
}