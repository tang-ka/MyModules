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
            GetComponentInParent<ControlPage>().onChangeControlOption += SetControlOption;

            for (int i = 0; i < controlButtons.Length; i++)
            {
                int index = i;
                controlButtons[index].onClick.AddListener(() => Control((ControlButton)index));
            }
        }

        public void Control(ControlButton btn)
        {
            Vector3 direction = Vector3.zero;

            switch (btn)
            {
                case ControlButton.X_MINUS:
                    direction = Vector3.left;
                    break;

                case ControlButton.X_PLUS:
                    direction = Vector3.right;
                    break;

                case ControlButton.Y_MINUS:
                    direction = Vector3.down;
                    break;

                case ControlButton.Y_PLUS:
                    direction = Vector3.up;
                    break;

                case ControlButton.Z_MINUS:
                    direction = Vector3.back;
                    break;

                case ControlButton.Z_PLUS:
                    direction = Vector3.forward;
                    break;

                default:
                    break;
            }

            controlStrategy.ControlMethod(direction);
        }

        void SetControlOption(ControlOption option)
        {
            (controlStrategy as ControlStrategy).Option = option;
        }
    }
}
