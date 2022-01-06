using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathKid
{
    public class CustomSliderrEvent : UnityEvent<float, float>
    {
        public CustomSliderrEvent() { }
    }
    public class MainSetCustomSlider : MonoBehaviour
    {
        [SerializeField]
        SliderCustom Numerator_Slider;
        [SerializeField]
        SliderCustom Denomineter_slider;
        [SerializeField]
        private RectTransform prefab;
        [SerializeField]
        private RectTransform parent;
        [SerializeField]
        private Text Numerator_Denomineter;
        [SerializeField]
        private RectTransform filler;
        float Numerator = 0, Denomineter = 1;
        private List<RectTransform> partitions = new List<RectTransform>();

        public CustomSliderrEvent onValueChanged = new CustomSliderrEvent();

        public void StartCalculetion()
        {
            Numerator_Slider.onValueChanged.AddListener(OnValueChanged_Numerator);
            Denomineter_slider.onValueChanged.AddListener(OnValueChanged_Denomineter);
            onValueChanged?.Invoke(Numerator, Denomineter);
        }

        public void StopCalculetion()
        {
            Numerator_Slider.onValueChanged.RemoveAllListeners();
            Denomineter_slider.onValueChanged.RemoveAllListeners();
        }

        private void OnValueChanged_Numerator(float val)
        {
            Numerator = val;
            OnValueUpdatedNumerator();
            onValueChanged?.Invoke(Numerator, Denomineter);
        }
        private void OnValueChanged_Denomineter(float val)
        {
            Denomineter = val;
            OnValueUpdatedNumerator();
            OnValueUpdatedDenomineter();
            onValueChanged?.Invoke(Numerator, Denomineter);
        }

        private void OnValueUpdatedDenomineter()
        {
            foreach (var line in partitions)
            {
                line.gameObject.SetActive(false);
            }
            if (Denomineter > 1)
            {
                if (partitions.Count >= Denomineter)
                {
                    if (Denomineter == 0)
                    {

                    }
                    else
                    {
                        UpdatePos((int)Denomineter);
                    }
                }
                else
                {
                    for (int i = 0; partitions.Count < Denomineter; i++)
                    {
                        var line = Instantiate<RectTransform>(prefab, parent) as RectTransform;
                        line.gameObject.SetActive(false);
                        partitions.Add(line);
                    }
                    UpdatePos((int)Denomineter);
                }
            }
        }

        private void UpdatePos(int partitionsCount)
        {
            var posOffset = parent.rect.width / partitionsCount;
            float newPos = 0;
            for (int i = 0; partitions.Count > 0 && i < partitionsCount - 1; i++)
            {
                if (partitionsCount - 1 == 1)
                {
                    newPos = posOffset;
                }
                else
                {
                    newPos = posOffset * (i + 1);
                }
                partitions[i].gameObject.SetActive(true);
                partitions[i].anchoredPosition = new Vector3(newPos, 0);
            }
        }

        private void OnValueUpdatedNumerator()
        {
            try
            {
                if ((Numerator / Denomineter) == 0)
                {
                    Numerator_Denomineter.text = "";
                    filler.sizeDelta = new Vector2(0, 0);
                }
                else
                {
                    Numerator_Denomineter.text = Numerator + "/" + Denomineter;
                    filler.sizeDelta = new Vector2(parent.rect.width * (Numerator / Denomineter), parent.rect.height);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Numerator_Denomineter.text = "";
                filler.sizeDelta = new Vector2(0, 0);
            }
        }

    }
}