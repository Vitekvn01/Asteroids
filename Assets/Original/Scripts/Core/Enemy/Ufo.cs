using System;
using UnityEngine;

namespace Original.Scripts.Core.Enemy
{
    public class Ufo : IEnemy
    {
        public bool IsActive { get; }
        
        public event Action<IEnemy> OnEnemyDeath;
        
        public void Death()
        {
            OnEnemyDeath?.Invoke(this);
        }

        public void Activate(Vector3 pos, Quaternion rotation)
        {
            throw new System.NotImplementedException();
        }

        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public void SetSpeed(float speed)
        {
            throw new NotImplementedException();
        }


    }
}