using UniRx;
using UnityEngine;

namespace Original.Scripts.Presentation.UI.ViewModel
{
    public class JoystickViewModel
    {
        private ReactiveProperty<Vector2> _direction = new ReactiveProperty<Vector2>(Vector2.zero);
        
        public IReadOnlyReactiveProperty<Vector2> Direction => _direction;
        public void SetDirection(Vector2 dir)
        {
            _direction.Value = dir;
        }

        public void Reset()
        {
            _direction.Value = Vector2.zero;
        }
    }
}