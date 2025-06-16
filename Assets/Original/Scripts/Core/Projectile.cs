using Original.Scripts.Core.Enemy;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.PlayerShip;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core
{
    public class Projectile : ITickable, IColliderHandler
    {
        private readonly CustomPhysics _physics;
    
        private IProjectileView _view;

        private float _speed;
        
        private float _lifetime;
        
        private float _timer;

        private bool _isActive;

        public bool IsActive => _isActive;

        public Projectile(IProjectileView view, CustomPhysics physics, float speed)
        {
            _view = view;
            _physics = physics;

            _speed = speed;
            
            _timer = 0f;
            _lifetime = 1f;

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
    
        public void OnTriggerEnter(ICustomCollider other)
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
