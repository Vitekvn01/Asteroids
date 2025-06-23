using System;
using Original.Scripts.Presentation.UI.View;
using UniRx;

namespace Original.Scripts.Presentation.UI.Binder
{
    public class ShipHUDBinder : IDisposable
    {
        private readonly ShipHUDView _view;
        private readonly ShipHUDViewModel _viewModel;
        private readonly CompositeDisposable _disposables = new();

        public ShipHUDBinder(ShipHUDView view, ShipHUDViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;

            Bind();
        }

        private void Bind()
        {
            _viewModel.Health
                .Subscribe(hp => _view.HealthText.text = $"Health: {hp}")
                .AddTo(_disposables);
            
            _viewModel.Position
                .Subscribe(pos => _view.PositionText.text = $"X: {pos.x:0.00} Y: {pos.y:0.00}")
                .AddTo(_disposables);

            _viewModel.Rotation
                .Subscribe(rot => _view.RotationText.text = $"Angle: {rot:0.0}°")
                .AddTo(_disposables);

            _viewModel.Speed
                .Subscribe(speed => _view.SpeedText.text = $"Speed: {speed:0.00}")
                .AddTo(_disposables);

            _viewModel.LaserAmmo
                .Subscribe(ammo => _view.LaserAmmoText.text = $"Ammo: {ammo}")
                .AddTo(_disposables);

            _viewModel.LaserCooldown
                .Subscribe(cd => _view.LaserCooldownText.text = $"Cooldown: {cd:0.0}s")
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}