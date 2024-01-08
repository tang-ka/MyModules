using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class ControlModeItem : MonoBehaviour
    {
        [SerializeField] Toggle toggle;
        [SerializeField] Text title;
        [SerializeField] Image icon;

        ControlPage controlPage;

        private void Awake()
        {
            title = GetComponentInChildren<Text>();
            icon = GetComponentInChildren<Image>();
        }

        public void SetToggleListener(ControlMode mode, ControlPage page)
        {
            controlPage = page;
            toggle.onValueChanged.AddListener((isOn) => ModedChange(isOn, mode));
        }

        private void ModedChange(bool isOn, ControlMode mode)
        {
            controlPage.ControlMode = mode;
            ActivateIconColor(isOn);
        }

        private void ActivateIconColor(bool isOn)
        {
            if (isOn)
            {

                icon.color = Define.SELECTED_MODE_IMG_COLOR;
            }
            else
            {

                icon.color = Define.DESELECTED_MODE_IMG_COLOR;
            }
        }
    } 
}
