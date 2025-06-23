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
        private readonly IScore _score;
        
        private readonly TickableManager _tickableManager;
        private readonly DisposableManager _disposableManager;
        
        [Inject]
        public UIFactory(ShipHUDView hudView, ScoreView scoreView, IScore score, TickableManager tickableManager, DisposableManager disposableManager)
        {
            _shipHUDView = hudView;
            _scoreView = scoreView;
            _score = score;
            _tickableManager = tickableManager;
            _disposableManager = disposableManager;
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
    }
}