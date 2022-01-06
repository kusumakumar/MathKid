using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathKid
{
    public class ManagerForCal : MonoBehaviour
    {
        [SerializeField]
        Set set1;
        [SerializeField]
        Set set2;
        [SerializeField]
        Toggle Compare;
        [SerializeField]
        Slider posUpdater;

        public Vector3 position1 = new Vector3(0, 0, 0);
        public Vector3 position2 = new Vector3(10, 10, 10);

        private void Start()
        {
            // Make sure the slider value is clamped between 0 and 1

        }


        private void OnEnable()
        {
            set1.StartCalculetion();
            set1.updateToManager.AddListener(UpdateCustom);
            set2.StartCalculetion();
            set2.updateToManager.AddListener(UpdateCustom);
            posUpdater.onValueChanged.AddListener(UpdatePosition);
            position1 = set1.main.GetComponent<RectTransform>().position;
            position2 = set2.main.GetComponent<RectTransform>().position;
        }
        private void OnDisable()
        {
            set1.StartCalculetion();
            set1.StopCalculetion();
            set2.StartCalculetion();
            set2.StopCalculetion();
        }

        public void UpdateCustom(float val)
        {

            if (set1.totalVal < set2.totalVal)
            {
                set1.status = "Smaller";
                set2.status = "Bigger";
            }
            else if (set1.totalVal > set2.totalVal)
            {
                set1.status = "Bigger";
                set2.status = "Smaller";
            }
            else if (set1.totalVal == set2.totalVal)
            {
                set1.status = "Same";
                set2.status = "Same";
            }
        }

        public void UpdatePosition(float value)
        {
            Vector3 newPosition = Vector3.Lerp(position1, position2, value);
            set1.main.GetComponent<RectTransform>().position = newPosition;
        }

    }
}
