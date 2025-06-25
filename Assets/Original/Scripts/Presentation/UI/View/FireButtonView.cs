using System;
using Original.Scripts.Core;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Original.Scripts.Presentation.UI.View
{
    public class FireButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private ShootType _type;

        private readonly Subject<(ShootType, bool)> _pressStream = new();

        public IObservable<(ShootType, bool)> OnPressStream => _pressStream;

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressStream.OnNext((_type, true));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressStream.OnNext((_type, false));
        }
    }
}