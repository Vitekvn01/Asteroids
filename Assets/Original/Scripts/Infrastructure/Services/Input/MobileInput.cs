using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Infrastructure.Services.Factories;
using Original.Scripts.Presentation.UI.ViewModel;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Input
{
    public class MobileInput : IInput
    {
        private readonly JoystickViewModel _joystick;
        
        [Inject]
        public MobileInput(UIFactory uiFactory)
        {
            _joystick = uiFactory.CreateJoystick();
        }

        public float GetAxisX() => _joystick.Direction.Value.x;
        public float GetAxisY() => _joystick.Direction.Value.y;

        public bool CheckPressedFirePrimary() => false;
        public bool CheckPressedFireSecondary() => false;
    }
}