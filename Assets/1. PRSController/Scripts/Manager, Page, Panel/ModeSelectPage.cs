using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class ModeSelectPage : Page
    {
        [SerializeField] Button btnMove;
        [SerializeField] Button btnRotate;
        [SerializeField] Button btnScale;

        protected override void Init()
        {
            base.Init();

            btnMove.onClick.AddListener(() => ChangeMode(ControlMode.Move));
            btnRotate.onClick.AddListener(() => ChangeMode(ControlMode.Rotate));
            btnScale.onClick.AddListener(() => ChangeMode(ControlMode.Scale));
        }

        private void ChangeMode(ControlMode mode)
        {
            if (parentController is PRSPageController)
            {
                (parentController as PRSPageController).OpenPage(PageState.Control, mode);
            }
        }
    }
}
