using TMPro;
using UnityEngine;

namespace Original.Scripts.Presentation.UI.View
{
    public class ShipHUDView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text positionText;
        [SerializeField] private TMP_Text rotationText;
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text laserAmmoText;
        [SerializeField] private TMP_Text laserCooldownText;
        
        public TMP_Text HealthText => healthText;
        public TMP_Text PositionText => positionText;
        public TMP_Text RotationText => rotationText;
        public TMP_Text SpeedText => speedText;
        public TMP_Text LaserAmmoText => laserAmmoText;
        public TMP_Text LaserCooldownText => laserCooldownText;
        
        public void Show() => _root.SetActive(true);
        public void Hide() => _root.SetActive(false);
    }
}