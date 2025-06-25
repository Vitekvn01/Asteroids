using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Original.Scripts.Presentation.UI.View
{
    public class JoystickView : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _handle;

        private RectTransform _bgRect;
        private float _radius;

        private readonly Subject<Vector2> _onDirection = new Subject<Vector2>();
        private readonly Subject<Unit> _onRelease = new Subject<Unit>();

        public IObservable<Vector2> OnDirection => _onDirection;
        public IObservable<Unit> OnRelease => _onRelease;

        private void Awake()
        {
            _bgRect = GetComponent<RectTransform>();

            float bgRadius = _bgRect.sizeDelta.x * 0.5f;
            float handleRadius = _handle.sizeDelta.x * 0.5f;
            _radius = bgRadius - handleRadius;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _bgRect, eventData.position, eventData.pressEventCamera, out var pos);

            Vector2 clamped = Vector2.ClampMagnitude(pos, _radius);
            _handle.anchoredPosition = clamped;

            Vector2 direction = clamped / _radius;
            _onDirection.OnNext(direction);
        }

        public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

        public void OnPointerUp(PointerEventData eventData)
        {
            _handle.anchoredPosition = Vector2.zero;
            _onRelease.OnNext(Unit.Default);
        }
    }
}