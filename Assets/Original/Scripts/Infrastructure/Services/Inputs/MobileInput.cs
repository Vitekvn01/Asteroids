using Original.Scripts.Core;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Signals.inputSignal;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Inputs
{
    public class MobileInput : IInput
    {
        private Vector2 _direction;
        private readonly SignalBus _signalBus;
        
        private bool _firePrimaryPressed;
        private bool _fireSecondaryPressed;
        
        public MobileInput(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<JoystickDirectionSignal>(OnJoystickDirectionChanged);
            _signalBus.Subscribe<ShootButtonSignal>(OnShootButtonSignal);
        }

        private void OnJoystickDirectionChanged(JoystickDirectionSignal signal)
        {
            _direction = signal.Direction;
        }
        
        private void OnShootButtonSignal(ShootButtonSignal signal)
        {
            if (signal.Type == ShootType.Primary)
                _firePrimaryPressed = signal.IsPressed;
            else if (signal.Type == ShootType.Secondary)
                _fireSecondaryPressed = signal.IsPressed;
        }

        public float GetAxisX() => _direction.x;
        public float GetAxisY() => _direction.y;

        public bool CheckPressedFirePrimary() => _firePrimaryPressed;
        public bool CheckPressedFireSecondary() => _fireSecondaryPressed;

        public void Dispose()
        {
            _signalBus.Unsubscribe<JoystickDirectionSignal>(OnJoystickDirectionChanged);
            _signalBus.Unsubscribe<ShootButtonSignal>(OnShootButtonSignal);
        }
    }
}