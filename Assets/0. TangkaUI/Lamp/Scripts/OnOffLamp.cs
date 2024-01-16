using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TangkaUI
{
    public class OnOffLamp : Lamp
    {
        [Header("Options")]
        public bool isOn;

        private Color onColor = Color.blue;
        private Color offColor = Color.red;

        protected Action<bool> onSwitchLamp;

        protected override void Init()
        {
            onSwitchLamp += ActivateBulb;
            base.Init();
        }

        protected override void Monitoring()
        {
            base.Monitoring();
        }

        private void ActivateBulb(bool _isOn)
        {
            isOn = _isOn;
            bulb.color = isOn ? onColor : offColor;
        }

        protected void SetLampColor(Color onColor, Color OffColor)
        {
            this.onColor = onColor;
            this.offColor = OffColor;
        }
        
    } 
}
