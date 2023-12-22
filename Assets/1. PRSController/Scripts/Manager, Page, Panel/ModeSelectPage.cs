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
        public void Init()
        {
            //btnMove.onClick.AddListener(() => ChangeMode());
        }

        private void ChangeMode(PageState state)
        {
            throw new NotImplementedException();
        }
    }
}
