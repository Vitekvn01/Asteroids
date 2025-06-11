using Original.Scripts.Core.Interfaces.IView;
using Original.Scripts.Core.Physics;
using UnityEngine;
using Zenject;

public class Projectile : ITickable, ICustomCollider
{
    private readonly CustomPhysics _physics;
    
    private IProjectileView _view;
    
    private float _lifetime;
    private float _timer;

    private bool _isActive;

    public bool IsActive => _isActive;
    
    public float Radius => 1;
    
    public object Owner => this;
    
    public bool IsTrigger { get; private set; }

    public Projectile(IProjectileView view, CustomPhysics physics, bool isTrigger = true)
    {
        _view = view;
        _physics = physics;
        _timer = 0f;
        _lifetime = 3f;
    }
    
    public void Tick()
    {
        if (_isActive)
        {
            Move(100);
        
            _view.Transform.position = _physics.Position;
            _view.Transform.rotation = Quaternion.Euler(0f, 0f, _physics.Rotation);
            
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
        _view.SetActive(true);
        _timer = 0;
        _isActive = true;
    }
    public void Deactivate()
    {
        _view.SetActive(false);
        _isActive = false;
    }

    private void Move(float speed)
    {
        Vector3 direction = _view.Transform.up; 
        _physics.AddForce(direction.normalized * speed);
    }



    public void OnTriggerEnter(ICustomCollider other)
    {
        throw new System.NotImplementedException();
    }

    public void OnTriggerStay(ICustomCollider other)
    {
        throw new System.NotImplementedException();
    }

    public void OnTriggerExit(ICustomCollider other)
    {
        throw new System.NotImplementedException();
    }
}
