using System;
using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Interfaces.IPhysics;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.Signals;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.Enemy
{
    public class Ufo : IEnemy, ITickable, IColliderHandler
    {
        private readonly SignalBus _signalBus;
        
        private readonly CustomPhysics _physics;
        
        private readonly IUfoView _view;

        private readonly IWeapon _weapon;

        private readonly float _stopRadius;
        
        private readonly float _fireRadius;

        private readonly float _fireSpreadAngle;
        
        private Ship _target;

        private float _speed;
        
        private bool _isActive;
        
        public EnemyType Type { get; }
        public CustomPhysics Physics => _physics;
        
        public bool IsActive => _isActive;
        
        
        public event Action<IEnemy> OnEnemyDeath;
        
        public Ufo(IUfoView view, SignalBus signalBus, CustomPhysics physics, IWeapon weapon, float speed, float stopRadius, 
            float fireRadius, float fireSpreadAngle, EnemyType type)
        {
            _view = view;
            _signalBus = signalBus;
            _physics = physics;
            
            _weapon = weapon;
            _speed = speed;
            _stopRadius = stopRadius;
            _fireRadius = fireRadius;
            _fireSpreadAngle = fireSpreadAngle;
            
            Type = type;
            
            _isActive = true;
        }
        
        public void Tick()
        {
            if (_isActive)
            {
                Vector3 directionToTarget = (Vector3)_target.Physics.Position - _view.Transform.position;
                float distance = directionToTarget.magnitude;
                
                Vector3 directionNormalized = directionToTarget.normalized;

                float angle = Vector2.SignedAngle(_view.Transform.up, directionNormalized);
                float newRotation = _view.Transform.eulerAngles.z + angle;
                
                _physics.SetRotation(newRotation);
                
                if (distance > _stopRadius)
                {
                    _physics.AddForce(_view.Transform.up * _speed);
                }
                
                if (distance <= _fireRadius)
                {
                    _weapon.Update();
                    
                    float randomAngleOffset = UnityEngine.Random.Range(-_fireSpreadAngle / 2f, _fireSpreadAngle / 2f);
                    Quaternion spreadRotation = Quaternion.Euler(0, 0, _view.Transform.eulerAngles.z + randomAngleOffset);

                    _weapon.TryShoot(_view.ShootPoint.position, spreadRotation, _physics.Velocity.magnitude);
                }
                
                _view.Transform.position = _physics.Position;
                _view.Transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);
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
            _signalBus.Fire(new EnemyDestroyedSignal(Type));
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