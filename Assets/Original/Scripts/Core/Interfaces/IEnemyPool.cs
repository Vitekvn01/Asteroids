using Original.Scripts.Core.Entity.Enemy;
using UnityEngine;

namespace Original.Scripts.Core.Interfaces
{
    public interface IEnemyPool
    {
        IEnemy Get(EnemyType type, Vector3 position, Quaternion rotation);
    }
}