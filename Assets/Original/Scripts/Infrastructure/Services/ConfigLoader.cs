using Original.Scripts.Core.Config;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Weapons;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services
{
    public class ConfigLoader : IConfigProvider
    {
        private const string ConfigFolder = "Config";
        public PlayerConfig PlayerConfig { get; }
        
        public WeaponConfig WeaponConfig { get; }

        public ConfigLoader()
        {
            PlayerConfig = LoadConfig<PlayerConfig>("PlayerConfig");
            WeaponConfig = LoadConfig<WeaponConfig>("WeaponConfig");
        }

        public T LoadConfig<T>(string path)
        {
            TextAsset json = Resources.Load<TextAsset>(path);
            return JsonUtility.FromJson<T>(json.text);
        }
    }
}