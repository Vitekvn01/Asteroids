using Original.Scripts.Core.Entity.PlayerShip;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Presentation.UI.Binder;
using Original.Scripts.Presentation.UI.View;
using Original.Scripts.Presentation.UI.ViewModel;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly ShipHUDView _shipHUDView;
        private readonly ScoreView _scoreView;
        private readonly StartWindowView _startWindowView; 
        private readonly IScore _score;
        
        private readonly TickableManager _tickableManager;
        private readonly DisposableManager _disposableManager;
        private readonly SignalBus _signalBus;
        
        [Inject]
        public UIFactory(ShipHUDView hudView, ScoreView scoreView, IScore score, TickableManager tickableManager,
            DisposableManager disposableManager, StartWindowView startWindowView, SignalBus signalBus)
        {
            _shipHUDView = hudView;
            _scoreView = scoreView;
            _startWindowView = startWindowView;
            _score = score;
            _tickableManager = tickableManager;
            _disposableManager = disposableManager;
            _signalBus = signalBus;
  
        }

        public void CreateShipHud(ShipController shipController)
        {
            var hudViewModel = new ShipHUDViewModel(shipController);
            _tickableManager.Add(hudViewModel);
            
            var shipHudBinder = new ShipHUDBinder(_shipHUDView, hudViewModel);
            _disposableManager.Add(shipHudBinder);
        }
        
        public void CreateScoreHud()
        {
            var scoreViewModel = new ScoreViewModel(_score);
            var scoreBinder = new ScoreViewBinder(_scoreView, scoreViewModel);
            _disposableManager.Add(scoreBinder);
        }

        public void CreateStartWindow()
        {
            var loseWindowViewModel = new StartWindowViewModel(_score, _signalBus);
            var loseWindowBinder = new StartWindowBinder(_startWindowView, loseWindowViewModel);
            _disposableManager.Add(loseWindowBinder);
        }
    }
}