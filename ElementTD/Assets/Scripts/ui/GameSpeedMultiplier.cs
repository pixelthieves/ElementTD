using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameSpeedMultiplier : MonoBehaviour
    {
        public float Min;
        public float Max;
        public float Step;

        public Text Output;

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            SetCurrentSpeed(1);
        }

        public void SpeedUp()
        {
            SetCurrentSpeed(Time.timeScale * Step);
        }

        public void SpeedDown()
        {
            SetCurrentSpeed(Time.timeScale / Step);
        }

        private void SetCurrentSpeed(float value)
        {
            Time.timeScale = Mathf.Clamp(value, Min, Max);
            Output.text = Time.timeScale + "x";
        }
    }
}