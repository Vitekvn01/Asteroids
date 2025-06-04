public class PhysicsSettings
{
    public float Drag { get; private set; }
    public float MaxSpeed { get; private set;}
    
    public PhysicsSettings(float drag, float maxSpeed)
    {
        Drag = drag;
        MaxSpeed = maxSpeed;
    }
}
