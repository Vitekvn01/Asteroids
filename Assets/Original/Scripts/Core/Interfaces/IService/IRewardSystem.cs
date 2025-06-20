using Original.Scripts.Core.Entity.Enemy;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IRewardSystem
    {
        void GiveReward(EnemyType type);
    }
}