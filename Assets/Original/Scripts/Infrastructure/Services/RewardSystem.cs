using System.Collections.Generic;
using Original.Scripts.Core.Entity.Enemy;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services
{
    public class RewardSystem : IRewardSystem
    {
        private readonly IScore _score;
        private readonly Dictionary<EnemyType, int> _rewardByType;
        public RewardSystem(IScore score, IConfigProvider configLoader)
        {
            _score = score;
            _rewardByType = new Dictionary<EnemyType, int>();

            foreach (var entry in configLoader.RewardConfig.Rewards)
            {
                if (!_rewardByType.ContainsKey(entry.EnemyType))
                {
                    _rewardByType.Add(entry.EnemyType, entry.RewardPoints);
                }
            }
        }

        public void GiveReward(EnemyType type)
        {
            if (_rewardByType.TryGetValue(type, out int reward))
            {
                _score.AddCount(reward);
                Debug.Log($"Rewarded {reward} for {type}. Total score: {_score.CurrentCount}");
            }
            else
            {
                Debug.LogWarning($"No reward defined for enemy type: {type}");
            }
        }
        
    }
}