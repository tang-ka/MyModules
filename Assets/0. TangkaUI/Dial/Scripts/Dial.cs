using PRSController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class Dial : MonoBehaviour
    {
        [SerializeField] DialHandle handle;
        public DialHandle Handle { get => handle; }

        [SerializeField] float dialValue;
        public float DialValue
        {
            get => dialValue;
            private set
            {
                dialValue = value;
                UpdateDisplay(dialValue);
                onValueChanged?.Invoke(dialValue);
            }
        }
        public Action<float> onValueChanged;

        [SerializeField] float unit = 0.01f;
        [SerializeField] string handleLayerName;
        [SerializeField] Text displayDialValue;

        private void Awake()
        {
            handle.receiveHandleValue += ((value) =>
            {
                dialValue = (value) * unit;
                DialValue = (float)Math.Round(dialValue, 3);
            });
            handle.Init(handleLayerName);
        }

        private void UpdateDisplay(float vlaue)
        {
            displayDialValue.text = string.Format("{0:0.00}", vlaue);
        }
    } 
}
