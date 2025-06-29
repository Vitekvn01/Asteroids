using System.Collections.Generic;
using Original.Scripts.Core.Interfaces.IPhysics;
using Zenject;

namespace Original.Scripts.Core.Physics
{
    public class ManualFixedTickManager : IManualFixedTickManager, IFixedTickable
    {
        private readonly List<IFixedTickable> _tickables = new();

        public void Register(IFixedTickable tickable)
        {
            if (!_tickables.Contains(tickable))
            {
                _tickables.Add(tickable);
            }
  
        }

        public void Unregister(IFixedTickable tickable)
        {
            _tickables.Remove(tickable);
        }

        public void FixedTick()
        {
            for (int i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].FixedTick();
            }
        }
    }
}