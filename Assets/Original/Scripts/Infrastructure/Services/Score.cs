using Original.Scripts.Core.Interfaces.IService;

namespace Original.Scripts.Infrastructure.Services
{
    public class Score : IScore
    {
        public int Count { get; private set; }

        public void AddCount(int count)
        {
            Count += count;
        }
    }
}