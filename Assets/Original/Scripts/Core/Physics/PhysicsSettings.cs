namespace Original.Scripts.Core.Physics
{
    public class PhysicsSettings
    {
        public float GlobalMaxSpeed { get; private set;}
    
        public PhysicsSettings(float globalMaxSpeed)
        {
            GlobalMaxSpeed = globalMaxSpeed;
        }
    }
}
