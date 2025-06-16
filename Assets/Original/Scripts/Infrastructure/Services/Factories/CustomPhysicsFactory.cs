using Original.Scripts.Core;
using Original.Scripts.Core.Interfaces.IService;
using Original.Scripts.Core.Physics;

using UnityEngine;
using Zenject;

namespace Original.Scripts.Infrastructure.Services.Factories
{
    public class CustomPhysicsFactory : ICustomPhysicsFactory
    {
        private readonly TickableManager _tickableManager;
        private readonly PhysicsSettings _settings;
        private readonly CollisionWord _collisionWord;
        private readonly WorldBounds _worldBounds;
    
        [Inject]
        public CustomPhysicsFactory(TickableManager tickableManager, PhysicsSettings physicsSettings, CollisionWord collisionWord, WorldBounds worldBounds)
        {
            _tickableManager = tickableManager;
            _settings = physicsSettings;
            _collisionWord = collisionWord;
            _worldBounds = worldBounds;
        }
        public CustomPhysics Create(Vector2 pos, float rotation, float drag, float bounce, ICustomCollider customCollider)
        {
            var created = new CustomPhysics(pos, rotation, drag, bounce, customCollider, _settings,_worldBounds );
            _collisionWord.Register(created);
            _tickableManager.Add(created);
            return created;
        }
    }
}