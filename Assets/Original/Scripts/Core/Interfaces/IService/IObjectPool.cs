using UnityEngine;

public interface IObjectPool<T>
{
    public T AddToPool();

    public T Get(Vector3 pos, float angleZ);
}