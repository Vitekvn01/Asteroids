using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Core.Interfaces
{
    public interface IEnemyPool
    {
        IEnemy Get(EnemyType type, Vector3 position, Quaternion rotation);
        IEnemy AddToPool(EnemyType type);
    }
}