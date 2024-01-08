using PRSController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class ModePanel : MonoBehaviour
    {
        [SerializeField] ControlModeItem tglMoveMode;
        [SerializeField] ControlModeItem tglRotateMode;
        [SerializeField] ControlModeItem tglScaleMode;

        private void Awake()
        {
            var controlPage = transform.parent.GetComponent<ControlPage>();
            tglMoveMode.SetToggleListener(PRSController.ControlMode.Move, controlPage);
            tglRotateMode.SetToggleListener(PRSController.ControlMode.Rotate, controlPage);
            tglScaleMode.SetToggleListener(PRSController.ControlMode.Scale, controlPage);
        }
    }
}