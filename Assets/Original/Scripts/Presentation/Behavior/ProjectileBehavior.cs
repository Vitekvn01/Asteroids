using Original.Scripts.Core.Interfaces.IView;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour, IProjectileView
{
    public Transform Transform => gameObject.transform;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
