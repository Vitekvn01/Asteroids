using Original.Scripts.Core.Config;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services
{
    public class ConfigLoader : IConfigProvider
    {
        private const string ConfigFolder = "Config";
        public PlayerConfig PlayerConfig { get; }
        
        public WeaponConfig WeaponConfig { get; }
        
        public AsteroidConfig AsteroidConfig { get; }
        
        public UfoConfig UfoConfig { get; }
        
        public WorldConfig WorldConfig { get; }

        public RewardConfig RewardConfig { get; }

        public ConfigLoader()
        {
            PlayerConfig = LoadConfig<PlayerConfig>("PlayerConfig");
            WeaponConfig = LoadConfig<WeaponConfig>("WeaponConfig");
            AsteroidConfig = LoadConfig<AsteroidConfig>("AsteroidConfig");
            UfoConfig = LoadConfig<UfoConfig>("UfoConfig");
            RewardConfig = LoadConfig<RewardConfig>("RewardConfig");
            WorldConfig =  LoadConfig<WorldConfig>("WorldConfig");
        }

        public T LoadConfig<T>(string path)
        {
            TextAsset json = Resources.Load<TextAsset>($"{ConfigFolder}/{path}");
            return JsonUtility.FromJson<T>(json.text);
        }
    }
}