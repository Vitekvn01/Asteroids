using System;
using Original.Scripts.Presentation.UI.View;
using Original.Scripts.Presentation.UI.ViewModel;
using UniRx;

namespace Original.Scripts.Presentation.UI.Binder
{
    public class FireButtonBinder : IDisposable
    {
        private readonly CompositeDisposable _disposables = new();

        public FireButtonBinder(FireButtonView view, FireButtonViewModel viewModel)
        {
            view.OnPressStream
                .Subscribe(signal  =>
                {
                    var (type, isPressed) = signal ;
                    viewModel.SetPressed(type, isPressed);
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}