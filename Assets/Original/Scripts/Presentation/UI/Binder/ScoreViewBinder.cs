using System;
using Original.Scripts.Presentation.UI.View;
using Original.Scripts.Presentation.UI.ViewModel;
using UniRx;

namespace Original.Scripts.Presentation.UI.Binder
{
    public class ScoreViewBinder : IDisposable
    {
        private readonly ScoreViewModel _viewModel;
        private readonly ScoreView _view;
        private readonly CompositeDisposable _disposables = new();

        public ScoreViewBinder(ScoreView view, ScoreViewModel viewModel)
        {
            _viewModel = viewModel;
            _view = view;

            _viewModel.Score
                .Subscribe(value => _view.ScoreText.text = $"Score: {value}")
                .AddTo(_disposables);
            _viewModel.MaxScore
                .Subscribe(value => _view.MaxScoreText.text = $"Record: {value}")
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}