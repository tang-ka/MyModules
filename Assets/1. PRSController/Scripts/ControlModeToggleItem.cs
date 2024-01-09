using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class ControlModeToggleItem : MonoBehaviour
    {
        [SerializeField] Toggle toggle;
        [SerializeField] Image background;
        [SerializeField] Image icon;

        ControlMode myMode;
        ControlPage controlPage;

        public void SetToggleListener(ControlMode mode, ControlPage page)
        {
            controlPage = page;
            myMode = mode;

            controlPage.onChangeControlMode += onChangeMode;

            toggle.isOn = false;
            ActivateToggleColor(false);
            toggle.onValueChanged.AddListener(ModeChange);
            toggle.isOn = (controlPage.ControlMode == myMode);
        }

        private void ModeChange(bool isOn)
        {
            if (isOn)
                controlPage.ControlMode = myMode;
        }

        private void ActivateToggleColor(bool isOn)
        {
            if (isOn)
            {
                background.color = Define.SELECTED_MODE_TOGGLE;
                icon.color = Define.SELECTED_MODE_ICON;
            }
            else
            {
                background.color = Define.DESELECTED_MODE_TOGGLE;
                icon.color = Define.DESELECTED_MODE_ICON;
            }
        }

        private void onChangeMode(ControlMode mode)
        {
            ActivateToggleColor(myMode == mode);
        }
    } 
}
