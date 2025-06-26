using Original.Scripts.Core.Interfaces.IService;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core
{
    public class WorldBounds
    {
        private readonly float _width;
        private readonly float _height;
        public float Width => _width;
        public float Height => _height;
        
        [Inject]
        public WorldBounds(Camera mainCamera, IConfigProvider configProvider)
        {
            _width = configProvider.WorldConfig.Width;
  
            _height = _width / mainCamera.aspect;
            
            float orthographicSize = _height / 2f; 
            mainCamera.orthographicSize = orthographicSize;
        }
        
        public Vector2 WrapPosition(Vector2 pos)
        {
            float x = Mathf.Repeat(pos.x + _width / 2f, _width) - _width / 2f;
            float y = Mathf.Repeat(pos.y + Height / 2f, Height) - Height / 2f;
            return new Vector2(x, y);
        }
    }
}