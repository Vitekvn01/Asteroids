using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.PlayerShip;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Enemy
{
    public class Ufo : IEnemy, ITickable, IColliderHandler
    {
        private readonly CustomPhysics _physics;
        
        private IUfoView _view;

        private IObjectPool<Projectile> _projectilePool;

        private Ship _target;

        private float _speed;
        
        private bool _isActive;
        
        public bool IsActive => _isActive;
        
        public event Action<IEnemy> OnEnemyDeath;
        
        public Ufo(IUfoView view, CustomPhysics physics, IObjectPool<Projectile> projectilePool, float speed)
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
                _view.Transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);
                
                Vector3 directionToPlayer = _view.Transform.up;
                
                _physics.SetVelocity(directionToPlayer * _speed);
                
                if (directionToPlayer.sqrMagnitude > 0.001f)
                {
                    float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                    _physics.SetRotation(targetAngle);
                }
            }
        }
        
        public void Activate(Vector3 pos, Quaternion rotation)
        {
            _view.Transform.position = pos;
            _view.Transform.rotation = rotation;
            
            _physics.SetPosition(pos);
            _physics.SetRotation(rotation.eulerAngles.z);
            
            _view.SetActive(true);
            
            _isActive = true;
        }
        
        public void Deactivate()
        {
            _view.SetActive(false);
            _physics.SetActive(false);
            _isActive = false;

        }

        public void SetTarget(Ship ship)
        {
            _target = ship;
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