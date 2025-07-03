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
            PlayerConfig = loader.LoadConfig<PlayerConfig>(nameof(Core.Config.PlayerConfig));
            WeaponConfig = loader.LoadConfig<WeaponConfig>(nameof(Core.Config.WeaponConfig));
            AsteroidConfig = loader.LoadConfig<AsteroidConfig>(nameof(Core.Config.AsteroidConfig));
            UfoConfig = loader.LoadConfig<UfoConfig>(nameof(Core.Config.UfoConfig));
            RewardConfig = loader.LoadConfig<RewardConfig>(nameof(Core.Config.RewardConfig));
            WorldConfig = loader.LoadConfig<WorldConfig>(nameof(Core.Config.WorldConfig));
        }

    }
}