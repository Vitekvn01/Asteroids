
namespace Original.Scripts.Core.StateMachine
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public void Exit();
    }
}
