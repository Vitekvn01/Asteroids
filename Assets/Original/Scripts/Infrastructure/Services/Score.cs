using System;
using Original.Scripts.Core.Interfaces.IService;

namespace Original.Scripts.Infrastructure.Services
{
    public class Score : IScore
    {
        public int MaxScore { get; private set; }
        public int CurrentCount { get; private set; }

        public event Action<int> OnChangedScoreEvent;
        
        public event Action<int> OnChangedMaxScoreEvent;
        
        public void AddCount(int count)
        {
            CurrentCount += count;
            OnChangedScoreEvent?.Invoke(CurrentCount);

            if (CurrentCount > MaxScore)
            {
                MaxScore = CurrentCount;
                OnChangedMaxScoreEvent?.Invoke(MaxScore);
            }
        }

        public void ResetCurrentScore()
        {
            CurrentCount = 0;
            OnChangedScoreEvent?.Invoke(CurrentCount);
        }
    }
}