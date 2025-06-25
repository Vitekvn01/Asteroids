using Original.Scripts.Core.Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Presentation.UI.ViewModel
{
    public class JoystickViewModel
    {
        private readonly SignalBus _signalBus;
        
        private ReactiveProperty<Vector2> _direction = new ReactiveProperty<Vector2>(Vector2.zero);

        public JoystickViewModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public IReadOnlyReactiveProperty<Vector2> Direction => _direction;
        public void SetDirection(Vector2 dir)
        {
            _signalBus.Fire(new JoystickDirectionSignal(dir));
        }

        public void Reset()
        {
            _signalBus.Fire(new JoystickDirectionSignal(Vector2.zero));
        }
    }
}