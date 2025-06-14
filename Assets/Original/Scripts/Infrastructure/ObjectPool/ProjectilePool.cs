using System.Collections.Generic;
using System.Linq;
using Original.Scripts.Core;
using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

public class ProjectilePool : IObjectPool<Projectile>
{
    private const int InitialSize = 100;
    
    private readonly Transform _parent;
    
    private readonly IProjectileFactory _projectileFactory;
    
    private readonly List<Projectile> _pool = new();
    
    [Inject]
    public ProjectilePool(IProjectileFactory projectileFactory, Transform parent = null) 
    {
        _projectileFactory = projectileFactory;
        _parent = parent;
        
        for (int i = 0; i < InitialSize; i++)
        {
            AddToPool();
        }

    }

    public Projectile AddToPool()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        float angleZ = 0;
        
        Projectile instance = _projectileFactory.Create(pos, angleZ, _parent);
        instance.Deactivate();
        
        _pool.Add(instance);
        return instance;
    }
    
    public Projectile Get(Vector3 pos, Quaternion rotation)
    {
        var instance = _pool.FirstOrDefault(p => !p.IsActive) ?? AddToPool();
        instance.Activate(pos, rotation);
        return instance;
    }
}