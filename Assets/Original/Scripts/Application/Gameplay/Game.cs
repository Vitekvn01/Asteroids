using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Original.Scripts.Application.Gameplay.Spawner;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals;
using UnityEngine;
using Zenject;

public class Game : IInitializable
{
    private readonly SignalBus _signalBus;

    private readonly IScore _score;
    private readonly PlayerSpawner _playerSpawner;
    private readonly EnemySpawner _enemySpawner;
    
    private readonly IUIFactory _uiFactory; 
    
    private bool _isRunning;

    [Inject]
    public Game(IScore score, PlayerSpawner playerSpawner, EnemySpawner enemySpawner, IUIFactory uiFactory, SignalBus signalBus)
    {
        _score = score;
        _playerSpawner = playerSpawner;
        _enemySpawner = enemySpawner;
        _uiFactory = uiFactory;
        _signalBus = signalBus;
    }
    
    
    public void Initialize()
    {
        _signalBus.Subscribe<StartGameSignal>(OnStartGameSignal);
        _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDeadSignal);
        _uiFactory.CreateStartWindow();
        _uiFactory.CreateScoreHud();
        _uiFactory.CreateShipHud(_playerSpawner.ShipController);

    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
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
