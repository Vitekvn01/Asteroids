using System;
using Original.Scripts.Core.Interfaces.IPhysics;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.Signals;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.Enemy
{
    public class Debris : IEnemy, IColliderHandler, ITickable
    {
        private readonly SignalBus _signalBus;
        
        private readonly CustomPhysics _physics;
        
        private IAsteroidView _view;

        private float _speed;
        
        private bool _isActive;
        
        public bool IsActive => _isActive;
        
        public EnemyType Type { get; }
        public CustomPhysics Physics => _physics;
        
        public event Action<IEnemy> OnEnemyDeath;
        
        public Debris(IAsteroidView view, SignalBus signalBus, CustomPhysics physics, float speed, EnemyType type)
        {
            _view = view;
            _signalBus = signalBus;
            _physics = physics;
            _speed = speed;
            Type = type;
        }
        
        public void Tick()
        {
            if (_isActive)
            {
                _view.Transform.position = _physics.Position;
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

        public void SetSpeed(float speedParent)
        {
            _speed = speedParent * 2;
            Vector2 direction = _view.Transform.up;
            _physics.SetVelocity(direction * (_speed + speedParent));
        }
        
        public void Death()
        {
            OnEnemyDeath?.Invoke(this);
            _signalBus.Fire(new EnemyDestroyedSignal(Type));
            Deactivate();
        }

        public void OnTriggerEnter(ICustomCollider other)
        {

        }

        public void OnCollisionEnter(ICustomCollider other)
        {
        }


    }
}