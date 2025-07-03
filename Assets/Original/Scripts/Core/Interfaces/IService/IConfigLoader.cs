namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IConfigLoader
    {
        public T LoadConfig<T>(string path);
    }
}