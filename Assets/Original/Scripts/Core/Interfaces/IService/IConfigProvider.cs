using Original.Scripts.Core.Config;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IConfigProvider
    {
        public PlayerConfig PlayerConfig { get; }
        T LoadConfig<T>(string path);
    }
}