using UnityEngine;

namespace Original.Scripts.Core.Signals
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