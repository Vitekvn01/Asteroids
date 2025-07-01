using System;
using Original.Scripts.Core.Interfaces.IService;

namespace Original.Scripts.Infrastructure.Services
{
    public class Score : IScore
    {
        public int MaxScore { get; private set; }
        public int CurrentScore { get; private set; }

        public event Action<int> OnChangedScoreEvent;
        
        public event Action<int> OnChangedMaxScoreEvent;
        
        public void AddCount(int count)
        {
            CurrentScore += count;
            OnChangedScoreEvent?.Invoke(CurrentScore);

            if (CurrentScore > MaxScore)
            {
                MaxScore = CurrentScore;
                OnChangedMaxScoreEvent?.Invoke(MaxScore);
            }
        }

        public void ResetCurrentScore()
        {
            CurrentScore = 0;
            OnChangedScoreEvent?.Invoke(CurrentScore);
        }
    }
}