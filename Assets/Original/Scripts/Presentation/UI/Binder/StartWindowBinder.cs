using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals;
using Original.Scripts.Presentation.UI.View;
using Original.Scripts.Presentation.UI.ViewModel;
using UniRx;
using Zenject;

namespace Original.Scripts.Presentation.UI.Binder
{
    public class StartWindowBinder : IDisposable
    {
        private readonly StartWindowView _view;
        private readonly StartWindowViewModel _viewModel;
        private readonly CompositeDisposable _disposables = new();

        public StartWindowBinder(StartWindowView view, StartWindowViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;

            _view.Show();

            _view.StartButton.onClick.AddListener(_viewModel.OnStartButtonClicked);
            
            _viewModel.Score
                .Subscribe(value => _view.ScoreText.text = $"Score: {value}")
                .AddTo(_disposables);
            _viewModel.MaxScore
                .Subscribe(value => _view.MaxScoreText.text = $"Record: {value}")
                .AddTo(_disposables);
            
            _viewModel.PlayerDead
                .Subscribe(_ =>
                {
                    _view.Show();
                })
                .AddTo(_disposables);

            _viewModel.StartClicked
                .Subscribe(_ => _view.Hide())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _view.StartButton.onClick.RemoveListener(_viewModel.OnStartButtonClicked);
            _disposables.Dispose();
        }
    }
}