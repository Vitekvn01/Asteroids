using System;
using System.Collections.Generic;
using Original.Scripts.Core.Entity.Enemy;

namespace Original.Scripts.Core.Config
{   
    
    [Serializable]
    public class RewardConfig
    {
        public List<EnemyRewardEntry> Rewards;
    }
    
    [Serializable]
    public class EnemyRewardEntry
    {
        public EnemyType EnemyType;
        public int RewardPoints;
    }
}