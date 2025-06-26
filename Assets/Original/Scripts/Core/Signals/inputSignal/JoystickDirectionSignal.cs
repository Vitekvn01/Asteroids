using UnityEngine;

namespace Original.Scripts.Core.Signals.inputSignal
{
    public class JoystickDirectionSignal
    {
        public Vector2 Direction;

        public JoystickDirectionSignal(Vector2 direction)
        {
            Direction = direction;
        }
    }
}