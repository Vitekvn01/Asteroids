using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services
{
    public class ConfigLoader : IConfigLoader
    {
        private const string ConfigFolder = "Config";
        
        public T LoadConfig<T>(string path)
        {
            TextAsset json = Resources.Load<TextAsset>($"{ConfigFolder}/{path}");
            return JsonUtility.FromJson<T>(json.text);
        }
    }
}