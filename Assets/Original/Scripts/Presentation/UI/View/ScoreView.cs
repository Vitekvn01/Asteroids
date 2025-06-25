using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Original.Scripts.Presentation.UI.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _maxScoreText;
        [SerializeField] private TMP_Text _ScoreText;
        
        public TMP_Text MaxScoreText => _maxScoreText;
        public TMP_Text ScoreText => _ScoreText;
        

    }
}