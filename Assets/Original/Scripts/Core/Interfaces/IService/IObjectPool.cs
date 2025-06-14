using UnityEngine;

namespace Original.Scripts.Core.Interfaces.IService
{
    public interface IObjectPool<T>
    {
        public T AddToPool();

        public T Get(Vector3 pos, Quaternion rotation);
    }
}