using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomPhysicsFactory
{
    public CustomPhysics Create(Vector2 pos);
}