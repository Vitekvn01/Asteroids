using Original.Scripts.Core.StateMachine;
using UnityEngine;

namespace Original.Scripts.Core.Entity.Enemy.Ufo
{
    public class UfoMoveState : IState
    {
        private readonly UfoStateMachine _stateMachine;
        
        private readonly Ufo _ufo;
        
        public UfoMoveState(UfoStateMachine stateMachine, Ufo ufo)
        {
            _ufo = ufo;
            _stateMachine = stateMachine;
        }

        public void Enter() {}

        public void Update()
        {
            _ufo.RotateToTarget();
            
            Vector3 directionToTarget = (Vector3)_ufo.Target.Physics.Position - _ufo.View.Transform.position;
            float distance = directionToTarget.magnitude;
            
            _ufo.Physics.AddForce(_ufo.View.Transform.up * _ufo.Speed);

            if (distance <= _ufo.FireRadius)
            {
                _stateMachine.ChangeState(_stateMachine.UfoAttackState);
            }
        }

        public void Exit()
        {
            
        }
    }
}