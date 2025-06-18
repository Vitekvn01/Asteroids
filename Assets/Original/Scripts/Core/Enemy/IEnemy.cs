using System;
using System.Numerics;
using Original.Scripts.Core.Interfaces;
using Original.Scripts.Core.Physics;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

namespace Original.Scripts.Core.Enemy
{
    public interface IEnemy : IActivatable 
    {   
        public CustomPhysics Physics { get; } 
        public event Action<IEnemy> OnEnemyDeath;
        
        public void SetSpeed(float speed);

        public void Death();
    }
}