using PRSController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class ModePanel : Panel
    {
        [SerializeField] ControlModeToggleItem tglMoveMode;
        [SerializeField] ControlModeToggleItem tglRotateMode;
        [SerializeField] ControlModeToggleItem tglScaleMode;

        private void Awake()
        {
            var controlPage = transform.parent.GetComponent<ControlPage>();
            tglMoveMode.SetToggleListener(ControlMode.Move, controlPage);
            tglRotateMode.SetToggleListener(ControlMode.Rotate, controlPage);
            tglScaleMode.SetToggleListener(ControlMode.Scale, controlPage);
        }
    }
}