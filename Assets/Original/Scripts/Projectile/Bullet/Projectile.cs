using UnityEngine;
using Zenject;

public class Projectile : ITickable
{
    private readonly CustomPhysics _physics;
    
    private ProjectileBehavior _behavior;
    
    private float _lifetime;
    private float _timer;

    public Projectile(ProjectileBehavior behavior, CustomPhysics physics)
    {
        _behavior = behavior;
        _physics = physics;
        
        _timer = 0f;
        _lifetime = 3f;
    }

    public void Move(float speed)
    {
        Vector3 direction = _behavior.transform.up; 
        _physics.AddForce(direction.normalized * speed);
    }

    public void Tick()
    {
        Move(100);
        
        _behavior.transform.position = _physics.Position;
        _behavior.transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);
        
        Debug.Log("TickProjectile");
        _timer += Time.deltaTime;
        
        if (_timer > _lifetime)
        {
        }
    }
}
