using Original.Scripts.Core.Config;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IConfigProvider
    {
        public PlayerConfig PlayerConfig { get; }
        public WeaponConfig WeaponConfig { get; }
        public AsteroidConfig AsteroidConfig { get; }
        public UfoConfig UfoConfig { get; }
        public WorldConfig WorldConfig { get; }
        public RewardConfig RewardConfig { get; }
        
    }
}