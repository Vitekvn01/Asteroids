using UnityEngine;
using Zenject;

public class CustomPhysicsFactory : ICustomPhysicsFactory
{
    private readonly TickableManager _tickableManager;
    private readonly PhysicsSettings _settings;
    
    [Inject]
    public CustomPhysicsFactory(TickableManager tickableManager, PhysicsSettings physicsSettings)
    {
        _tickableManager = tickableManager;
        _settings = physicsSettings;
    }
    public CustomPhysics Create(Vector2 pos, float rotation)
    {
        var created = new CustomPhysics(pos, rotation, _settings);
        _tickableManager.Add(created);
        return created;
    }
}