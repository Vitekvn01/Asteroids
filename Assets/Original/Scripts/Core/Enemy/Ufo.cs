using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Enemy
{
    public class Ufo : IEnemy, ITickable, IColliderHandler
    {
        private readonly CustomPhysics _physics;
        
        private IAsteroidView _view;

        private IObjectPool<Projectile> _projectilePool;

        private Transform _targetT;

        private float _speed;
        
        private bool _isActive;
        
        public bool IsActive => _isActive;
        
        public event Action<IEnemy> OnEnemyDeath;
        
        public Ufo(IAsteroidView view, CustomPhysics physics, IObjectPool<Projectile> projectilePool, float speed)
        {
            _view = view;
            _physics = physics;
            _projectilePool = projectilePool;
            _speed = speed;
        }
        
        public void Tick()
        {
            if (_isActive)
            {
                _view.Transform.position = _physics.Position;
                
                /*Vector3 directionToPlayer = (_playerPosition - _view.Transform.position).normalized;*/

                // Устанавливаем скорость в сторону игрока
                /*_physics.SetVelocity(directionToPlayer * _speed);*/
            }
        }
        
        public void Activate(Vector3 pos, Quaternion rotation)
        {
            _view.Transform.position = pos;
            _view.Transform.rotation = rotation;
                
            _physics.SetPosition(pos);
            _physics.SetRotation(rotation.eulerAngles.z);
            
            Vector3 direction = _view.Transform.up;
            _physics.SetVelocity(direction.normalized * _speed);
            _physics.SetActive(true);
            
            _view.SetActive(true);
            
            _isActive = true;
        }
        
        public void Deactivate()
        {
            _view.SetActive(false);
            _physics.SetActive(false);
            _isActive = false;

        }

        public void SetSpeed(float speeed)
        {
            _speed = speeed;
        }
        
        public void Death()
        {
            OnEnemyDeath?.Invoke(this);
            Deactivate();
            Debug.Log("Projectile death");
        }
        
        public void OnTriggerEnter(ICustomCollider other)
        {
            Debug.Log("asteriod trigger");
        }
        
        public void OnCollisionEnter(ICustomCollider other)
        {
            Debug.Log("asteriod collision");
        }
    }
}