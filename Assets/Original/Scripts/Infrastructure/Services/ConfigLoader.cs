using Original.Scripts.Core.Config;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services
{
    public class ConfigLoader : IConfigProvider
    {
        private PlayerConfig _playerConfig;

        public PlayerConfig PlayerConfig { get; }

        public ConfigLoader()
        {
            PlayerConfig = LoadConfig<PlayerConfig>("PlayerConfig");
        }

        public T LoadConfig<T>(string path)
        {
            TextAsset json = Resources.Load<TextAsset>(path);
            return JsonUtility.FromJson<T>(json.text);
        }
    }
}