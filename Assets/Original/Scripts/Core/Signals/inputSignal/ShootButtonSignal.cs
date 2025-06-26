namespace Original.Scripts.Core.Signals.inputSignal
{
    public class ShootButtonSignal
    {
        public ShootType Type;
        public bool IsPressed;

        public ShootButtonSignal(ShootType type, bool isPressed)
        {
            Type = type;
            IsPressed = isPressed;
        }
    }
}