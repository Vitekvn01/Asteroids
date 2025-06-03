using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const string AxisX = "Horizontal";
    private const string AxisY = "Vertical";

    public float GetAxisX()
    {
        return Input.GetAxisRaw(AxisX);
    }

    public float GetAxisY()
    {
        return Input.GetAxisRaw(AxisY);
    }

    public bool CheckGetButtonDown(string nameInput)
    {
        return Input.GetButtonDown(nameInput);
    }

    public void Tick()
    {
        
    }
}
