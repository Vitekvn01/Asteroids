namespace Original.Scripts.Core.Interfaces.IService
{
   public interface IInput
   {
      public float GetAxisX();

      public float GetAxisY();

      public bool CheckPressedFirePrimary();

      public bool CheckPressedFireSecondary();
   }
}
