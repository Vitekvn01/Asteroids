using Original.Scripts.Core.StateMachine;
using UnityEngine;

namespace Original.Scripts.Core.Entity.Enemy.Ufo
{
    public class UfoIdleState : IState
    {
        private readonly UfoStateMachine _stateMachine;
        private readonly Ufo _ufo;

        public UfoIdleState(UfoStateMachine stateMachine, Ufo ufo)
        {
            _stateMachine = stateMachine;
            _ufo = ufo;
        }

        public void Enter()
        {
            Vector2 velocitySpeed = new Vector2(0,0);
            _ufo.Physics.SetVelocity(velocitySpeed);
        }

        public void Update()
        {
            if (_ufo.Target != null)
            {
                _stateMachine.ChangeState(_stateMachine.UfoMoveState);
            }
        }

        public void Exit()
        {
        }
    }
}