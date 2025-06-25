using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Serialization;

namespace Original.Scripts.Presentation.UI.View
{
    public class StartWindowView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _maxScoreText;

        public Button StartButton => _startButton;
        
        public TMP_Text MaxScoreText => _maxScoreText;
        public TMP_Text ScoreText => _scoreText;
        public void Show() => _root.SetActive(true);
        public void Hide() => _root.SetActive(false);
    }
}