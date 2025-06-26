using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals;
using Zenject;

namespace Original.Scripts.Application.Gameplay
{
    public class RewardHandler : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IRewardSystem _rewardSystem;

        public RewardHandler(SignalBus signalBus, IRewardSystem rewardSystem)
        {
            _signalBus = signalBus;
            _rewardSystem = rewardSystem;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyDestroyedSignal>(OnEnemyDestroyed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyDestroyedSignal>(OnEnemyDestroyed);
        }

        private void OnEnemyDestroyed(EnemyDestroyedSignal signal)
        {
            _rewardSystem.GiveReward(signal.EnemyType);
        }
    }
}