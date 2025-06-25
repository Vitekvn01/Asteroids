using System;
using Original.Scripts.Presentation.UI.View;
using Original.Scripts.Presentation.UI.ViewModel;
using UniRx;

namespace Original.Scripts.Presentation.UI.Binder
{
    public class JoystickBinder : IDisposable
    {
        private readonly JoystickView _view;
        private readonly JoystickViewModel _viewModel;

        private CompositeDisposable _disposables = new CompositeDisposable();

        public JoystickBinder(JoystickView view, JoystickViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;

            Bind();
        }

        public void Bind()
        {
            _view.OnDirection
                .Subscribe(dir => _viewModel.SetDirection(dir))
                .AddTo(_disposables);

            _view.OnRelease
                .Subscribe(_ => _viewModel.Reset())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}