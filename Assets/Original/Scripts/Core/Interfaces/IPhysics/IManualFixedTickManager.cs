using Zenject;

namespace Original.Scripts.Core.Interfaces.IPhysics
{
    public interface IManualFixedTickManager
    {
        void Register(IFixedTickable tickable);
    }
}