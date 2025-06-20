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

        public ConfigLoader()
        {
            PlayerConfig = LoadConfig<PlayerConfig>("PlayerConfig");
            WeaponConfig = LoadConfig<WeaponConfig>("WeaponConfig");
            AsteroidConfig = LoadConfig<AsteroidConfig>("AsteroidConfig");
            UfoConfig = LoadConfig<UfoConfig>("UfoConfig");
        }

        public T LoadConfig<T>(string path)
        {
            TextAsset json = Resources.Load<TextAsset>($"{ConfigFolder}/{path}");
            return JsonUtility.FromJson<T>(json.text);
        }
    }
}