using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services.Inputs
{
    public class XboxInput : IInput
    {
        private const string AxisX = "Joystick_Horizontal";
        private const string AxisY = "Joystic_Vertical";
        
        private const string FirePrimary = "Joystick_Fire1";
        private const string FireSecondary = "Joystick_Fire2";


        public float GetAxisX()
        {
            return Input.GetAxisRaw(AxisX);
        }
    
        public float GetAxisY()
        {
            return Input.GetAxisRaw(AxisY);
        }
    
        public bool CheckPressedFirePrimary()
        {
            return Input.GetButton(FirePrimary);
        }
        
        public bool CheckPressedFireSecondary()
        {
            return Input.GetButton(FireSecondary);
        }
    }
}