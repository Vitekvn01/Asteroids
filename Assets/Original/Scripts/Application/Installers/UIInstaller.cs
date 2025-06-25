using Original.Scripts.Presentation.UI.View;
using Original.Scripts.Presentation.UI.ViewModel;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private Canvas _uICanvas;
    
    [SerializeField] private ShipHUDView _shipHUDView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private StartWindowView startWindowView;
    [SerializeField] private JoystickView _joystickView;
    
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
            .FromInstance(startWindowView);

        Container.Bind<JoystickView>()
            .FromInstance(_joystickView);
    }
    
}