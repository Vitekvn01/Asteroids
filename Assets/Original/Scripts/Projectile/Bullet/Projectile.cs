using UnityEngine;
using Zenject;

public class Projectile : ITickable
{
    private readonly CustomPhysics _physics;
    
    private ProjectileBehavior _behavior;
    
    private float _lifetime;
    private float _timer;

    private bool _isActive;

    public bool IsActive => _isActive;

    public Projectile(ProjectileBehavior behavior, CustomPhysics physics)
    {
        _behavior = behavior;
        _physics = physics;
        
        _timer = 0f;
        _lifetime = 3f;
    }
    
    public void Tick()
    {
        if (_isActive)
        {
            Move(100);
        
            _behavior.transform.position = _physics.Position;
            _behavior.transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);
            
            _timer += Time.deltaTime;
        
            if (_timer > _lifetime)
            {
                Deactivate();
            }
        }
    }
    
    public void Activate(Vector3 pos, float angleZ)
    {
        _physics.SetVelocity(new Vector3(0, 0, 0));
        _physics.SetPosition(pos);
        _physics.SetRotation(angleZ);
        _behavior.gameObject.SetActive(true);
        _timer = 0;
        _isActive = true;
    }
    public void Deactivate()
    {
        _behavior.gameObject.SetActive(false);
        _isActive = false;
    }

    private void Move(float speed)
    {
        Vector3 direction = _behavior.transform.up; 
        _physics.AddForce(direction.normalized * speed);
    }

   
}
