using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Original.Scripts.Application.Gameplay.Spawner;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

public class Game : IInitializable
{
    private readonly PlayerSpawner _playerSpawner;
    private readonly EnemySpawner _enemySpawner;
    private readonly IUIFactory _uiFactory; 
    private readonly IInput _input;

    private bool _isRunning;

    [Inject]
    public Game(PlayerSpawner playerSpawner, EnemySpawner enemySpawner, IUIFactory uiFactory, IInput input )
    {
        _playerSpawner = playerSpawner;
        _enemySpawner = enemySpawner;
        
        _uiFactory = uiFactory;
        
        _input = input;

    }

    public void Initialize()
    {
        _uiFactory.CreateScoreHud();
        
        _playerSpawner.Spawn();
        _playerSpawner.ShipController.Ship.OnDeathEvent += _enemySpawner.Stop;
        _enemySpawner.Start();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void Dispose()
    {
        _playerSpawner.ShipController.Ship.OnDeathEvent -= _enemySpawner.Stop;
    }
}
