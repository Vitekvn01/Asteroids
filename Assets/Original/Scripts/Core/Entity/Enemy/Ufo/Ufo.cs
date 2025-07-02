using System;
using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Entity.Weapons;
using Original.Scripts.Core.Interfaces.IPhysics;
using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using Original.Scripts.Core.Signals;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Entity.Enemy.Ufo
{
    public class Ufo : IEnemy, ITickable, IColliderHandler
    {
        private readonly SignalBus _signalBus;
        
        private readonly CustomPhysics _physics;

        private readonly IWeapon _weapon;
        
        private readonly UfoStateMachine _ufoStateMachine;
        
        public IUfoView View { get; }
        public float FireRadius { get; }
        public float FireSpreadAngle { get; }
        public float Speed { get; private set; }
        public Ship Target { get; private set; }
        public bool IsActive { get; private set; }
        public EnemyType Type { get; }
        public CustomPhysics Physics => _physics;

        public IWeapon Weapon => _weapon;
        
        public event Action<IEnemy> OnEnemyDeath;

        public Ufo(
            IUfoView view,
            SignalBus signalBus,
            CustomPhysics physics,
            IWeapon weapon,
            float speed,
            float fireRadius,
            float fireSpreadAngle,
            EnemyType type)
        {
            View = view;
            _signalBus = signalBus;
            _physics = physics;
            _weapon = weapon;
            Speed = speed;
            FireRadius = fireRadius;
            FireSpreadAngle = fireSpreadAngle;
            Type = type;

            IsActive = true;
            
            _ufoStateMachine = new UfoStateMachine(this);
        }
        
        public void Tick()
        {
            if (IsActive)
            {
                _ufoStateMachine.Update();
                SyncViewTransform();
            }
        }
        
        public void RotateToTarget()
        {
            Vector3 dir = (Vector3)Target.Physics.Position - View.Transform.position;
            Vector3 dirNorm = dir.normalized;
            float angle = Vector2.SignedAngle(View.Transform.up, dirNorm);
            float newRotation = View.Transform.eulerAngles.z + angle;
            Physics.SetRotation(newRotation);
        }

        private void SyncViewTransform()
        {
            View.Transform.position = Physics.Position;
            View.Transform.rotation = Quaternion.Euler(0f, 0f, Physics.Rotation);
        }

        public void Activate(Vector3 pos, Quaternion rotation)
        {
            View.Transform.position = pos;
            View.Transform.rotation = rotation;
            
            _physics.SetPosition(pos);
            _physics.SetRotation(rotation.eulerAngles.z);
            _physics.SetActive(true);
            View.SetActive(true);
            
            IsActive = true;
        }
        
        public void Deactivate()
        {
            View.SetActive(false);
            _physics.SetActive(false);
            IsActive = false;
        }

        public void SetTarget(Ship ship)
        {
            Target = ship;
        }

        public void SetSpeed(float speeed)
        {
            Speed = speeed;
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
        }
        
        public void OnCollisionEnter(ICustomCollider other)
        {
        }
    }
}