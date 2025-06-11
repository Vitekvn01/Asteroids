using Original.Scripts.Core.Interfaces.IView;
using UnityEngine;

public interface IShipView : IPhysicsView
{
    public Transform ShootPoint { get; }
    public void Death();

}
