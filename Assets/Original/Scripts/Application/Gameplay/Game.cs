using Original.Scripts.Application.Gameplay.Spawner;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Application.Gameplay
{
    public class Game : IInitializable
    {
        private readonly SignalBus _signalBus;

        private readonly IScore _score;
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;
    
        private readonly IUIFactory _uiFactory; 
    
        private bool _isRunning;

        [Inject]
        public Game(IScore score, PlayerSpawner playerSpawner, EnemySpawner enemySpawner, IUIFactory uiFactory,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _score = score;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _uiFactory = uiFactory;

        }
    
    
        public void Initialize()
        {
            _signalBus.Subscribe<StartGameSignal>(OnStartGameSignal);
            _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDeadSignal);
        
            _uiFactory.CreateStartWindow();
            _uiFactory.CreateScoreHud();
            _uiFactory.CreateShipHud(_playerSpawner.ShipController);
            
            if (UnityEngine.Application.platform == RuntimePlatform.Android)
            {
                _uiFactory.CreateMobileInput();
            }
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
        }
    }
}
