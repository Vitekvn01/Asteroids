using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;

namespace Original.Scripts.Infrastructure.Services.Inputs
{
    public class DesktopInput : IInput
    {
        private const string AxisX = "Horizontal";
        private const string AxisY = "Vertical";
    
        private const string FirePrimary = "Fire1";
        private const string FireSecondary = "Fire2";


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
            Debug.Log( "firePrimary " + Input.GetButton(FirePrimary));
            return Input.GetButton(FirePrimary);
        }
    
        public bool CheckPressedFireSecondary()
        {
            Debug.Log( "firePrimary " + Input.GetButton(FireSecondary));
            return Input.GetButton(FireSecondary);
        }
    }
}
