using Original.Scripts.Core.Enemy;
using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IEnemyFactory
    {
        public IEnemy Create(EnemyType enemyType, Vector3 position, float rotation = 0, Transform parent = null);
    }
}