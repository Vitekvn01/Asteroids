using UnityEngine;

namespace Original.Scripts.Core.Interfaces
{
    public interface IActivatable
    {
        public bool IsActive { get; }
        
        public void Activate(Vector3 pos, Quaternion rotation);
        
        public void Deactivate();
    }
}