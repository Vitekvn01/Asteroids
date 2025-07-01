using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.Mock
{
    public class MockAnalyticsService : IAnalyticsStrategy, IInitializable
    {
        public void Initialize()
        {
            Debug.Log("[MockAnalytics] Initialized");
        }

        public void LogGameLoaded()
        {
            Debug.Log("[MockAnalytics] game_loaded");
        }

        public void LogPlayerDied(int score)
        {
            Debug.Log($"[MockAnalytics] player_died | Score: {score}");
        }
    }
}