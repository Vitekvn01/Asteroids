using System;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals;
using UniRx;
using Zenject;

namespace Original.Scripts.Presentation.UI.ViewModel
{
    public class ScoreViewModel : IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IScore _score;
        
        private readonly Subject<Unit> _onStartPlay = new();
        private readonly Subject<Unit> _onPlayerDead = new();
        public IObservable<Unit> OnStartPlay => _onStartPlay;
        public IObservable<Unit> PlayerDead => _onPlayerDead;
        public ReactiveProperty<int> Score { get; } = new();
        public ReactiveProperty<int> MaxScore { get; } = new();

        private readonly CompositeDisposable _disposables = new();

        public ScoreViewModel(SignalBus signalBus, IScore score)
        {
            _signalBus = signalBus;
            _score = score;
            _score.OnChangedScoreEvent += OnChangedScore;
            _score.OnChangedMaxScoreEvent += OnChangedMaxScore;
            Score.Value = _score.CurrentScore;
            MaxScore.Value = _score.MaxScore;
            
            _signalBus.Subscribe<PlayerDeadSignal>(OnPlayerDead);
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
        }

        private void OnChangedScore(int newScore)
        {
            Score.Value = newScore;
        }
        
        private void OnChangedMaxScore(int newScore)
        {
            MaxScore.Value = newScore;
        }

        public void Dispose()
        {
            _score.OnChangedScoreEvent -= OnChangedScore;
            _score.OnChangedMaxScoreEvent -= OnChangedMaxScore;
            _disposables.Dispose();
        }
        
        private void OnStartGame(StartGameSignal signal)
        {
            _onStartPlay.OnNext(Unit.Default);
        }
    
        private void OnPlayerDead(PlayerDeadSignal signal)
        {
            _onPlayerDead.OnNext(Unit.Default);
        }
    }
}