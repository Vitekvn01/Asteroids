using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Entity.Projectiles;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity
{
    public class Projectile : ITickable, IColliderHandler
    {
        private const float Lifetime = 1f;
        
        private readonly CustomPhysics _physics;
    
        private IProjectileView _view;

        private float _speed;
        
        private float _timer;

        private bool _isActive;
        
        public CustomPhysics Physics => _physics;

        public bool IsActive => _isActive;
        
        public ProjectileType ProjectileType { get; }


        public Projectile(IProjectileView view, CustomPhysics physics, float speed, ProjectileType type)
        {
            _view = view;
            _physics = physics;

            _speed = speed;

            ProjectileType = type;
            
            _timer = 0f;

        }
    
        public void Tick()
        {
            if (_isActive)
            {
                Debug.DrawRay(_view.Transform.position, _physics.Velocity.normalized * 10, Color.red);
                Debug.DrawRay(_view.Transform.position, _view.Transform.up * 10, Color.green);
                _view.Transform.position = _physics.Position;
                _view.Transform.rotation =  Quaternion.Euler(0, 0, _physics.Rotation);
                _timer += Time.deltaTime;
        
                if (_timer > Lifetime)
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
            
            if (other.Handler is Ship ship)
            {
                Debug.Log("projetcile ship trigger");
            }
            
            Deactivate();
            Debug.Log("projetcile trigger");
        }

        public void OnCollisionEnter(ICustomCollider other)
        {
            Debug.Log("projetcile collision");
        }
    
    }
}
