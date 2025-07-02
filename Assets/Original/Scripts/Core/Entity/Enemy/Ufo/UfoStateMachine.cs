namespace Original.Scripts.Core.Entity.Enemy.Ufo
{
    public class UfoStateMachine : StateMachine.StateMachine
    {
        public UfoMoveState UfoMoveState { get; }
        public UfoAttackState UfoAttackState { get; }
        public UfoStateMachine(Ufo ufo)
        {
            UfoMoveState = new UfoMoveState(this, ufo);
            UfoAttackState = new UfoAttackState(this, ufo);
            
            ChangeState(UfoMoveState);
        }
    }
}