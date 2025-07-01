using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals;
using UniRx;
using Zenject;

namespace Original.Scripts.Presentation.UI.ViewModel
{
    public class StartWindowViewModel : IDisposable
    {
        private readonly IScore _score;
        private readonly SignalBus _signalBus;

        private readonly Subject<Unit> _startClicked = new();
        private readonly Subject<Unit> _onPlayerDead = new();

        public IObservable<Unit> StartClicked => _startClicked;
        public IObservable<Unit> PlayerDead => _onPlayerDead;

        public ReactiveProperty<int> Score { get; } = new();
        public ReactiveProperty<int> MaxScore { get; } = new();

        public StartWindowViewModel(IScore score, SignalBus signalBus)
        {
            _score = score;
            _signalBus = signalBus;

            _score.OnChangedScoreEvent += OnScoreChanged;
            _score.OnChangedMaxScoreEvent += OnMaxScoreChanged;

            Score.Value = _score.CurrentScore;
            MaxScore.Value = _score.MaxScore;

            _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);

            StartClicked
                .Subscribe(_ => _signalBus.Fire<StartGameSignal>());
        }

        private void OnScoreChanged(int newScore)
        {
            Score.Value = newScore;
        }

        private void OnMaxScoreChanged(int newMaxScore)
        {
            MaxScore.Value = newMaxScore;
        }

        private void OnPlayerDead(PlayerDeadSignal signal)
        {
            _onPlayerDead.OnNext(Unit.Default);
        }

        public void OnStartButtonClicked()
        {
            _startClicked.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _score.OnChangedScoreEvent -= OnScoreChanged;
            _score.OnChangedMaxScoreEvent -= OnMaxScoreChanged;
            _signalBus.Unsubscribe<PlayerDeadSignal>(OnPlayerDead);
        }
    }
}