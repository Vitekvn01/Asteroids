using System.Collections.Generic;
using Original.Scripts.Core.Enemy;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services
{
    public class RewardSystem : IRewardSystem
    {
        private readonly IScore _score;
        private readonly Dictionary<EnemyType, int> _rewardByType = new()
        {
            { EnemyType.Asteroid, 10 },
            { EnemyType.Debris, 1 },
            { EnemyType.Ufo, 50 }
        };

        public RewardSystem(IScore score)
        {
            _score = score;
        }

        public void GiveReward(EnemyType type)
        {
            if (_rewardByType.TryGetValue(type, out int reward))
            {
                _score.AddCount(reward);
                Debug.Log($"Rewarded {reward} for {type}. Total score: {_score.Count}");
            }
            else
            {
                Debug.LogWarning($"No reward defined for enemy type: {type}");
            }
        }
        
    }
}