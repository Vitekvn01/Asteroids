using Original.Scripts.Core.Config;
using Original.Scripts.Core.Interfaces.IService;

namespace Original.Scripts.Infrastructure.Services
{
    public class ConfigProvider : IConfigProvider
    {
        public PlayerConfig PlayerConfig { get; }
        public WeaponConfig WeaponConfig { get; }
        public AsteroidConfig AsteroidConfig { get; }
        public UfoConfig UfoConfig { get; }
        public WorldConfig WorldConfig { get; }
        public RewardConfig RewardConfig { get; }

        public ConfigProvider(IConfigLoader loader)
        {
            PlayerConfig = loader.LoadConfig<PlayerConfig>(nameof(PlayerConfig));
            WeaponConfig = loader.LoadConfig<WeaponConfig>(nameof(WeaponConfig));
            AsteroidConfig = loader.LoadConfig<AsteroidConfig>(nameof(AsteroidConfig));
            UfoConfig = loader.LoadConfig<UfoConfig>(nameof(UfoConfig));
            RewardConfig = loader.LoadConfig<RewardConfig>(nameof(RewardConfig));
            WorldConfig = loader.LoadConfig<WorldConfig>(nameof(WorldConfig));
        }

    }
}