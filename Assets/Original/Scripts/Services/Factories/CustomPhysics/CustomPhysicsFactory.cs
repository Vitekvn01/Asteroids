using UnityEngine;
using Zenject;

public class CustomPhysicsFactory : ICustomPhysicsFactory
{
    private readonly TickableManager _tickableManager;
    private readonly PhysicsSettings _settings;
    private readonly CollisionWord _collisionWord;
    
    [Inject]
    public CustomPhysicsFactory(TickableManager tickableManager, PhysicsSettings physicsSettings, CollisionWord collisionWord)
    {
        _tickableManager = tickableManager;
        _settings = physicsSettings;
        _collisionWord = collisionWord;
    }
    public CustomPhysics Create(Vector2 pos, float rotation, float radius)
    {
        var created = new CustomPhysics(pos, rotation, radius, _settings, _collisionWord);
        /*_collisionWord.Register(created);*/
        _tickableManager.Add(created);
        return created;
    }
}