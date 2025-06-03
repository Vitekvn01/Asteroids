using UnityEngine;
using Zenject;

public class CustomPhysicsFactory : ICustomPhysicsFactory
{
    private readonly TickableManager _tickableManager;
    private readonly PhysicsSettings _settings;
    
    [Inject]
    public CustomPhysicsFactory(TickableManager tickableManager, PhysicsSettings _physicsSettings)
    {
        _tickableManager = tickableManager;
        _settings = _physicsSettings;
    }
    public CustomPhysics Create(Vector2 pos)
    {
        var created = new CustomPhysics(pos, _settings);
        _tickableManager.Add(created);
        return created;
    }
}