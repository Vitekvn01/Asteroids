using System;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IScore
    {
        public int Count { get;}
        
        public event Action<int> OnChangedScoreEvent;
        public void AddCount(int count);
    }
}