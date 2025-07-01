namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IAnalyticsStrategy
    {
        public void LogGameLoaded();

        public void LogPlayerDied(int score);
    }
}