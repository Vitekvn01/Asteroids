using TMPro;
using UnityEngine;

namespace Original.Scripts.Presentation.UI.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        public TMP_Text ScoreText => scoreText;
    }
}