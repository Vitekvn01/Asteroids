using System;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IScore
    {
        public int MaxScore { get; }
        public int CurrentCount { get;}
        
        public event Action<int> OnChangedScoreEvent;
        
        public event Action<int> OnChangedMaxScoreEvent;
        public void AddCount(int count);
        public void ResetCurrentScore();
    }
}