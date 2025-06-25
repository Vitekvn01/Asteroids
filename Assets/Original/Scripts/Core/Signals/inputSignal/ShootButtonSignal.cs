namespace Original.Scripts.Core.Signals
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