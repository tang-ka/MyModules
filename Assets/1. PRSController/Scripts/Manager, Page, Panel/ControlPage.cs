using System;
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

        public Action<ControlMode> onChangeControlMode;

        [SerializeField]
        private ControlMode controlMode = ControlMode.None;
        public ControlMode ControlMode
        {
            get => controlMode;
            set 
            { 
                controlMode = value;
                ControlModeSetting();
                onChangeControlMode?.Invoke(controlMode);
            }
        }

        private bool isLocal;
        public bool IsLocal
        {
            get => isLocal;
            set
            {
                isLocal = value;
                onChangeControlOption?.Invoke(isLocal);
            }
        }
        public Action<bool> onChangeControlOption;

        protected override void Init()
        {
            base.Init();

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
                    controlPanel.ControlStrategy = new PositionStrategy(parent.data, isLocal);
                    break;
                case ControlMode.Rotate:
                    controlPanel.ControlStrategy = new RotationStrategy(parent.data, isLocal);
                    break;
                case ControlMode.Scale:
                    controlPanel.ControlStrategy = new ScaleStrategy(parent.data, isLocal);
                    break;
                default:
                    break;
            }
        }
    }
}
