using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
    public class OptionPanel : Panel
    {
        [SerializeField] SlideToggle tglWorldLocal;
        ControlPage parentPage;

        private void Awake()
        {
            tglWorldLocal.onValueChanged.AddListener(IsLocal);
            parentPage = GetComponentInParent<ControlPage>();
        }

        private void IsLocal(bool isOn)
        {
            parentPage.IsLocal = isOn;
        }
    } 
}
