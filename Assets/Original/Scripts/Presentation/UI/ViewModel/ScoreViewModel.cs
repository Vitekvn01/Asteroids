using System;
using Original.Scripts.Core.Interfaces.IService;
using UniRx;
using UnityEditor.Hardware;

namespace Original.Scripts.Presentation.UI.ViewModel
{
    public class ScoreViewModel : IDisposable
    {
        private readonly IScore _score;
        public ReactiveProperty<int> Score { get; } = new();
        public ReactiveProperty<int> MaxScore { get; } = new();

        private readonly CompositeDisposable _disposables = new();

        public ScoreViewModel(IScore score)
        {
            _score = score;
            _score.OnChangedScoreEvent += OnChangedScore;
            _score.OnChangedMaxScoreEvent += OnChangedMaxScore;
            Score.Value = _score.CurrentCount;
            MaxScore.Value = _score.MaxScore;
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
    }
}