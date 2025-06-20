using Original.Scripts.Core.Entity.Enemy;

namespace Original.Scripts.Core.Signals
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