using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace MathKid
{
    public class SliderEvent : UnityEvent<float>
    {
        public SliderEvent() { }
    }
    [RequireComponent(typeof(Slider))]
    public class SliderCustom : MonoBehaviour
    {
        [SerializeField]
        Text val_disp;
        Slider slider;
        public SliderEvent onValueChanged = new SliderEvent();

        private void Awake()
        {
            slider = this.GetComponent<Slider>();
        }
        private void OnEnable()
        {
            slider.onValueChanged.AddListener(ListenerMethod);
            slider.onValueChanged.Invoke(slider.value);
        }
        public void ListenerMethod(float value)
        {
            val_disp.text = value.ToString();
            onValueChanged?.Invoke(value);
        }
        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(ListenerMethod);
        }
    }
}