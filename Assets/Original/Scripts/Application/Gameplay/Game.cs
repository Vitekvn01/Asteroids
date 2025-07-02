using Original.Scripts.Application.Gameplay.Spawner;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Application.Gameplay
{
    public class Game : MonoBehaviour, IInitializable
    {
        private SignalBus _signalBus;
        
        private IScore _score;
        private PlayerSpawner _playerSpawner;
        private EnemySpawner _enemySpawner;
        
        private IUIFactory _uiFactory;
        private IAdsStrategy _adsService;
        private IAnalyticsStrategy _analytics;

        [Inject]
        public void Construct(IScore score, PlayerSpawner playerSpawner, EnemySpawner enemySpawner,
            IUIFactory uiFactory, IAdsStrategy adsService, IAnalyticsStrategy analytics,  SignalBus signalBus)
        {
            _signalBus = signalBus;
            _score = score;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _uiFactory = uiFactory;
            _adsService = adsService;
            _analytics = analytics;
            
            _signalBus.Subscribe<StartGameSignal>(OnStartGameSignal);
            _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDeadSignal);
        }
        
        public void Initialize()
        {
            _uiFactory.CreateStartWindow();
            _uiFactory.CreateScoreHud();
            _uiFactory.CreateShipHud(_playerSpawner.ShipController);
            
            if (UnityEngine.Application.platform == RuntimePlatform.Android)
            {
                _uiFactory.CreateMobileInput();
            }
        }

        public void Start()
        {
            _analytics.LogGameLoaded();
            _adsService.ShowInterstitial();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGameSignal);
            _signalBus.Unsubscribe<PlayerDeadSignal>(OnPlayerDeadSignal);
        }
        
        private void OnStartGameSignal()
        {
            _playerSpawner.Spawn();
            _enemySpawner.Start();
            _score.ResetCurrentScore();
            
        }

        private void OnPlayerDeadSignal()
        {
            _enemySpawner.Stop();
            _adsService.ShowInterstitial();
            _analytics.LogPlayerDied(_score.CurrentScore);
        }
    }
}
