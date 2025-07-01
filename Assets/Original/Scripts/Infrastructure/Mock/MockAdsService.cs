using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Mock
{
    public class MockAdsService : IAdsStrategy
    {
        public void ShowInterstitial()
        {
            Debug.Log("[MockAds] ShowInterstitial (mock)");
        }
    }
}