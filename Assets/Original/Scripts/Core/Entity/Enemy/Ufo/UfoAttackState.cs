using Original.Scripts.Core.StateMachine;
using UnityEngine;

namespace Original.Scripts.Core.Entity.Enemy.Ufo
{
    public class UfoAttackState : IState
    {
        private readonly UfoStateMachine _stateMachine;
        private readonly Ufo _ufo;

        public UfoAttackState(UfoStateMachine stateMachine, Ufo ufo)
        {
            _stateMachine = stateMachine;
            _ufo = ufo;
        }

        public void Enter()
        {
            if (_ufo.Target == null)
            {
                _stateMachine.ChangeState(_stateMachine.UfoIdleState);
            }
            
            Vector2 velocitySpeed = new Vector2(0,0);
            _ufo.Physics.SetVelocity(velocitySpeed);
        }

        public void Update()
        {
            if (_ufo.Target != null)
            {
                _ufo.RotateToTarget();
            
                _ufo.Weapon.Update();
                    
                float randomAngleOffset = Random.Range(-_ufo.FireSpreadAngle / 2f, _ufo.FireSpreadAngle / 2f);
                Quaternion spreadRotation = Quaternion.Euler(0, 0, _ufo.View.Transform.eulerAngles.z + randomAngleOffset);

                _ufo.Weapon.TryShoot(_ufo.View.ShootPoint.position, spreadRotation, _ufo.Physics.Velocity.magnitude);
            
                Vector3 directionToTarget = (Vector3)_ufo.Target.Physics.Position - _ufo.View.Transform.position;
                float distance = directionToTarget.magnitude;
            
                if (distance > _ufo.FireRadius)
                {
                    _stateMachine.ChangeState(_stateMachine.UfoMoveState);
                }
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.UfoIdleState);
            }

        }

        public void Exit()
        {
            if (_ufo.Target == null)
            {
                _stateMachine.ChangeState(_stateMachine.UfoIdleState);
            }
        }
    }
}