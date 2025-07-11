using Original.Scripts.Presentation.UI.View;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Application.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _uICanvas;
    
        [SerializeField] private ShipHUDView _shipHUDView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private StartWindowView _startWindowView;
        [SerializeField] private JoystickView _joystickView;
        [SerializeField] private FireButtonPrimaryView _fireButtonPrimary;
        [SerializeField] private FireButtonSecondaryView _fireButtonSecondary;
        public override void InstallBindings()
        {
            BindUIPrefabs();
        }
    
        private void BindUIPrefabs()
        {
            Container.Bind<Canvas>()
                .FromInstance(_uICanvas);
        
            Container.Bind<ShipHUDView>()
                .FromInstance(_shipHUDView);

            Container.Bind<ScoreView>()
                .FromInstance(_scoreView);

            Container.Bind<StartWindowView>()
                .FromInstance(_startWindowView);

            Container.Bind<JoystickView>()
                .FromInstance(_joystickView);

            Container.Bind<FireButtonPrimaryView>()
                .FromInstance(_fireButtonPrimary);
        
            Container.Bind<FireButtonSecondaryView>()
                .FromInstance(_fireButtonSecondary);
        
        }
    
    }
}