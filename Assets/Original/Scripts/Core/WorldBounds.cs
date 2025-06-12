using UnityEngine;

namespace Original.Scripts.Core
{
    public class WorldBounds
    {
        private float _width;
        private float _height;
        public float Width => _width;
        public float Height => _height;

        /*public WorldBounds(float width, float height)
        {
            _width = width;
            _height = height;
        }*/
        
        public WorldBounds(Camera cam)
        {
            CalculateBoundsFromCamera(cam);
        }
        
        private void CalculateBoundsFromCamera(Camera cam)
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            _width = width;
            _height = height;
        }

        public Vector2 WrapPosition(Vector2 pos)
        {
            float x = Mathf.Repeat(pos.x + _width / 2f, _width) - _width / 2f;
            float y = Mathf.Repeat(pos.y + Height / 2f, Height) - Height / 2f;
            return new Vector2(x, y);
        }
    }
}