using System;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public enum ControlButton
    {
        X_MINUS = 0,
        X_PLUS = 1,
        Y_MINUS = 2,
        Y_PLUS = 3,
        Z_MINUS = 4,
        Z_PLUS = 5
    }

    public class XYZControlPanel : Panel
    {
        [SerializeField] Button[] controlButtons;

        IControlStrategy controlStrategy;
        public IControlStrategy ControlStrategy
        {
            get => controlStrategy;
            set { controlStrategy = value; }
        }

        private void Awake()
        {
            controlButtons = GetComponentsInChildren<Button>();

            for (int i = 0; i < controlButtons.Length; i++)
            {
                int index = i;
                controlButtons[index].onClick.AddListener(() => Control((ControlButton)index));
            }
        }

        public void Control(ControlButton btn)
        {
            controlStrategy.ControlMethod(btn);
        }
    }
}
