using Original.Scripts.Core.Config;
using Original.Scripts.Core.Weapons;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IConfigProvider
    {
        public PlayerConfig PlayerConfig { get; }
        
        public WeaponConfig WeaponConfig { get; }
        
        public AsteroidConfig AsteroidConfig { get; }
        
        public UfoConfig UfoConfig { get; }
        T LoadConfig<T>(string path);
    }
}