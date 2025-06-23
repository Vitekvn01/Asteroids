using System;
using Original.Scripts.Core.Interfaces.IService;

namespace Original.Scripts.Infrastructure.Services
{
    public class Score : IScore
    {
        public int Count { get; private set; }

        public event Action<int> OnChangedScoreEvent;
        
        public void AddCount(int count)
        {
            Count += count;
            OnChangedScoreEvent?.Invoke(Count);
        }
    }
}