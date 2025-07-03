using TMPro;
using UnityEngine;

namespace Original.Scripts.Presentation.UI.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        
        [SerializeField] private TMP_Text _maxScoreText;
        [SerializeField] private TMP_Text _ScoreText;
        
        public TMP_Text MaxScoreText => _maxScoreText;
        public TMP_Text ScoreText => _ScoreText;
        
        public void Show() => _root.SetActive(true);
        public void Hide() => _root.SetActive(false);
    }
}