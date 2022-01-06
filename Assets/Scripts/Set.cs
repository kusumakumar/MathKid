using UnityEngine;
using UnityEngine.UI;

namespace MathKid
{

    public class Set : MonoBehaviour
    {
        [SerializeField]
        public MainSetCustomSlider main;
        [SerializeField]
        private Text compare_val;

        public float totalVal = 0;
        public SliderEvent updateToManager = new SliderEvent();
        public string status
        {
            get
            {
                return compare_val.text;
            }
            set
            {
                compare_val.text = value;
            }
        }

        public void StartCalculetion()
        {
            main.gameObject.SetActive(true);
            main.onValueChanged.AddListener(OnValueChanged_CustomSlider);
            main.StartCalculetion();
        }

        public void StopCalculetion()
        {
            main.gameObject.SetActive(true);
            main.onValueChanged.RemoveAllListeners();
            main.StopCalculetion();
        }

        private void OnValueChanged_CustomSlider(float numerator, float denomineter)
        {
            totalVal = (numerator / denomineter);
            updateToManager?.Invoke(totalVal);
        }
    }
}