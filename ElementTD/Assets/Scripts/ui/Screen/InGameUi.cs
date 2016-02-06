using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class InGameUi : MonoBehaviour
    {
        public Button PauseButton;
        public Button SlowDownButton;
        public Text CurrentSpeedText;
        public Button SpeedUpButton;
        public Text LivesText;
        public Text GoldText;
        public Text WaveText;
        public Text InterestText;
        public Image NextWaveImage;
        public Text NextWaveText;
        public Button NextWaveButton;
        public RectTransform TowerGrid;

        private GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public void Update()
        {
            LivesText.text = player.GetComponent<Health>().CurrentHealth.ToString("0");
            GoldText.text = player.GetComponent<Wallet>().Gold.ToString("0");
        }
    }
}