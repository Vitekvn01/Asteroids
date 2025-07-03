using System;

namespace Original.Scripts.Core.Config
{
    [Serializable]
    public class WorldConfig
    {
        public float Width;
        
        public int InitialSpawnCount;
        public int SpawnIntervalSeconds;
        public int MaxEnemies;
        public int DebrisSpawnCount;
        public float UfoSpawnChance ;

    }
}