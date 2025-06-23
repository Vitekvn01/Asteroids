using UnityEngine;

namespace Original.Scripts.Core
{
    public class WorldBounds
    {
        private float _width;
        private float _height;
        public float Width => _width;
        public float Height => _height;
        
        
        public WorldBounds( float width, Camera cam)
        {
            _width = width;
  
            _height = width / cam.aspect;
            
            float orthographicSize = _height / 2f; 
            cam.orthographicSize = orthographicSize;
            
        }
        
        public Vector2 WrapPosition(Vector2 pos)
        {
            float x = Mathf.Repeat(pos.x + _width / 2f, _width) - _width / 2f;
            float y = Mathf.Repeat(pos.y + Height / 2f, Height) - Height / 2f;
            return new Vector2(x, y);
        }
    }
}