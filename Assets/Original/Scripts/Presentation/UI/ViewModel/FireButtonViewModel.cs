using Original.Scripts.Core;
using Original.Scripts.Core.Signals;
using Original.Scripts.Core.Signals.inputSignal;
using Zenject;

namespace Original.Scripts.Presentation.UI.ViewModel
{
    public class FireButtonViewModel
    {
        private readonly SignalBus _signalBus;

        public FireButtonViewModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void SetPressed(ShootType type, bool isPressed)
        {
            _signalBus.Fire(new ShootButtonSignal(type, isPressed));
        }
    }
}