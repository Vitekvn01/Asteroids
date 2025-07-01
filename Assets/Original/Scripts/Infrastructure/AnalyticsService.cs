using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure
{
    public class AnalyticsService : IAnalyticsStrategy, IInitializable
    {
        public void Initialize()
        {
            Initilization();
        }
        
        public void LogGameLoaded()
        {
            FirebaseAnalytics.LogEvent("game_loaded", new Parameter[]
            {
                new Parameter("version", Application.version),
                new Parameter("platform", Application.platform.ToString())
            });
        }

        public void LogPlayerDied(int score)
        {
            FirebaseAnalytics.LogEvent("player_died", new Parameter[]
            {
                new Parameter("score", score),
            });
        }
        
        private void Initilization()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.Result == DependencyStatus.Available)
                {
                    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
                    Debug.Log("Firebase initialized and Analytics ready.");
                }
                else
                {
                    Debug.LogError($"Firebase error: {task.Result}");
                }
            });
        }
    }
}