using Original.Scripts.Core;
using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Presentation.UI.Binder;
using Original.Scripts.Presentation.UI.View;
using Original.Scripts.Presentation.UI.ViewModel;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _diContainer;

        private readonly Canvas _parent;
        
        private readonly ShipHUDView _shipHUDView;
        private readonly ScoreView _scoreView;
        private readonly StartWindowView _startWindowView; 
        private readonly JoystickView _joystickView;
        private readonly FireButtonView _fireButtonPrimaryView;
        private readonly FireButtonView _fireButtonSecondaryView;
        
        private readonly IScore _score;
        
        private readonly TickableManager _tickableManager;
        private readonly DisposableManager _disposableManager;
        private readonly SignalBus _signalBus;
        
        [Inject]
        public UIFactory(DiContainer diContainer, ShipHUDView hudView, ScoreView scoreView, JoystickView joystickView,
            IScore score,  TickableManager tickableManager, DisposableManager disposableManager,
            StartWindowView startWindowView, SignalBus signalBus, Canvas parent, 
            [Inject(Id = "Primary")] FireButtonView fireButtonPrimaryView,
            [Inject(Id = "Secondary")] FireButtonView fireButtonSecondaryView)
        {
            _shipHUDView = hudView;
            _scoreView = scoreView;
            _startWindowView = startWindowView;
            _joystickView = joystickView;
            
            _score = score;
            _tickableManager = tickableManager;
            _disposableManager = disposableManager;
            _signalBus = signalBus;
            _parent = parent;
             _fireButtonPrimaryView = fireButtonPrimaryView;
            _fireButtonSecondaryView = fireButtonSecondaryView;

            _diContainer = diContainer;
        }

        public void CreateShipHud(ShipController shipController)
        {
            var shipHudView = _diContainer.InstantiatePrefabForComponent<ShipHUDView>(_shipHUDView.gameObject,
                _parent.gameObject.transform);
            
            var hudViewModel = new ShipHUDViewModel(shipController);
            _tickableManager.Add(hudViewModel);
            
            var shipHudBinder = new ShipHUDBinder(shipHudView, hudViewModel);
            _disposableManager.Add(shipHudBinder);
        }
        
        public void CreateScoreHud()
        {
            var scoreView = _diContainer.InstantiatePrefabForComponent<ScoreView>(_scoreView.gameObject
                ,_parent.gameObject.transform);
            var scoreViewModel = new ScoreViewModel(_score);
            var scoreBinder = new ScoreViewBinder(scoreView, scoreViewModel);
            _disposableManager.Add(scoreBinder);
        }

        public void CreateStartWindow()
        {
            var startWindowView = _diContainer
                .InstantiatePrefabForComponent<StartWindowView>(_startWindowView.gameObject,_parent.gameObject.transform);
            var startWindowViewModel = new StartWindowViewModel(_score, _signalBus);
            var startWindowBinder = new StartWindowBinder(startWindowView, startWindowViewModel);
            _disposableManager.Add(startWindowBinder);
        }

        public void CreateMobileInput()
        {
            CreateJoystick();
            CreateFireButtonPrimary();
            CreateFireButtonSecondary();
        }

        private void CreateJoystick()
        {
            var joystickView = _diContainer
                .InstantiatePrefabForComponent<JoystickView>(_joystickView.gameObject, _parent.gameObject.transform);
            var joystickViewModel = new JoystickViewModel(_signalBus);
            var joystickBinder = new JoystickBinder(joystickView, joystickViewModel);
            _disposableManager.Add(joystickBinder);
            
            joystickView.gameObject.SetActive(true);
        }
        
        private void CreateFireButtonPrimary()
        {
            var fireButtonView = _diContainer.InstantiatePrefabForComponent<FireButtonView>(
                _fireButtonPrimaryView.gameObject, _parent.gameObject.transform);

            var fireButtonViewModel = new FireButtonViewModel(_signalBus);
            var fireButtonBinder = new FireButtonBinder(fireButtonView, fireButtonViewModel);
            _disposableManager.Add(fireButtonBinder);

            fireButtonView.gameObject.SetActive(true);
        }

        private void CreateFireButtonSecondary()
        {
            var fireButtonView = _diContainer.InstantiatePrefabForComponent<FireButtonView>(
                _fireButtonSecondaryView.gameObject, _parent.gameObject.transform);

            var fireButtonViewModel = new FireButtonViewModel(_signalBus);
            var fireButtonBinder = new FireButtonBinder(fireButtonView, fireButtonViewModel);
            _disposableManager.Add(fireButtonBinder);

            fireButtonView.gameObject.SetActive(true);
        }
    }
}