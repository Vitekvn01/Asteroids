using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Interfaces.IPhysics;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.Projectiles
{
    public class Projectile : ITickable, IColliderHandler
    {
        private readonly float _lifetime;
        private readonly float _speed;
        
        private readonly CustomPhysics _physics;
    
        private IProjectileView _view;
        
        private float _timer;

        private bool _isActive;
        
        public CustomPhysics Physics => _physics;

        public bool IsActive => _isActive;
        
        public ProjectileType Type { get; }


        public Projectile(IProjectileView view, CustomPhysics physics, float speed, float lifetime, ProjectileType type)
        {
            _view = view;
            _physics = physics;
            _lifetime = lifetime;
            _speed = speed;
            Type = type;

            _timer = 0f;

        }
    
        public void Tick()
        {
            if (_isActive)
            {
                _view.Transform.position = _physics.Position;
                _view.Transform.rotation =  Quaternion.Euler(0, 0, _physics.Rotation);
                _timer += Time.deltaTime;
        
                if (_timer > _lifetime)
                {
                    Deactivate();
                }
            }
        }
    
        public void Activate(Vector3 pos, Quaternion rotation)
        {
            _view.Transform.position = pos;
            _view.Transform.rotation = rotation;
            
            _physics.SetPosition(pos);
            _physics.SetRotation(rotation.eulerAngles.z);
            
            _physics.SetActive(true);
        
            _view.SetActive(true);
        
            _timer = 0;
            _isActive = true;
        }

        public void Lunch(float speedParent = 0)
        {
            Vector2 direction = Quaternion.Euler(0, 0, _view.Transform.rotation.eulerAngles.z) * Vector2.up;
            _physics.SetVelocity(direction * (_speed + speedParent));
        }

        public void Deactivate()
        {
            _view.SetActive(false);
            _physics.SetActive(false);
            _isActive = false;
        }
    
        public virtual void OnTriggerEnter(ICustomCollider other)
        {
            if (other.Handler is IEnemy enemy)
            {
                enemy.Death();
            }
            
            if (Type == ProjectileType.EnemyBullet)
            {
                if (other.Handler is Ship ship)
                {
                    ship.ApplyDamage();
                }
            }
            
            Deactivate();
        }

        public void OnCollisionEnter(ICustomCollider other)
        {
        }
    
    }
}
