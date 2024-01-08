using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public enum ControlMode
    {
        None = -1,
        Move,
        Rotate,
        Scale
    }

    public class ControlPage : Page
    {
        [SerializeField] XYZControlPanel controlPanel;
        [SerializeField] DifferentialIntervalPanel differnetialIntervalPanel;

        [SerializeField] Button btnClose;

        [SerializeField]
        private ControlMode controlMode = ControlMode.None;
        public ControlMode ControlMode
        {
            get => controlMode;
            set 
            { 
                controlMode = value;
                ControlModeSetting();
            }
        }

        protected override void Init()
        {
            base.Init();
            btnClose.onClick.AddListener(() => PRSControllerManager.Instance.Close());

            differnetialIntervalPanel.SetData(ref (parentController as PRSPageController).data);
        }

        private void OnDisable()
        {
            ControlMode = ControlMode.None;
        }

        void ControlModeSetting()
        {
            PRSPageController parent = parentController as PRSPageController;

            switch (ControlMode)
            {
                case ControlMode.None:
                    controlPanel.ControlStrategy = null;
                    break;
                case ControlMode.Move:
                    controlPanel.ControlStrategy = new PositionStrategy(parent.data);
                    break;
                case ControlMode.Rotate:
                    controlPanel.ControlStrategy = new RotationStrategy(parent.data);
                    break;
                case ControlMode.Scale:
                    controlPanel.ControlStrategy = new ScaleStrategy(parent.data);
                    break;
                default:
                    break;
            }
        }
    }
}
