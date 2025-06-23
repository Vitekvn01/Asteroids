using System;
using Original.Scripts.Core.Interfaces.IService;
using UniRx;
using UnityEditor.Hardware;

namespace Original.Scripts.Presentation.UI.ViewModel
{
    public class ScoreViewModel : IDisposable
    {
        private readonly IScore _score;
        public  ReactiveProperty<int> Score { get; } = new();

        private readonly CompositeDisposable _disposables = new();

        public ScoreViewModel(IScore score)
        {
            _score = score;
            _score.OnChangedScoreEvent += OnChangedScore;
            Score.Value = _score.Count;
        }

        private void OnChangedScore(int newScore)
        {
            Score.Value = newScore;
        }

        public void Dispose()
        {
            _score.OnChangedScoreEvent -= OnChangedScore;
            _disposables.Dispose();
        }
    }
}