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

    [Flags]
    public enum ControlOption
    {
        None = 0,
        Option_Local        = 1 << 0,
        Option_Constrained  = 1 << 1,
        Option_td = 1 << 2
    }

    public class ControlPage : Page
    {
        [SerializeField] XYZControlPanel controlPanel;
        [SerializeField] DifferentialIntervalPanel differnetialIntervalPanel;
        [SerializeField] public CommandHandler commandHandler;

        public Action<ControlMode> onChangeControlMode;

        [SerializeField]
        private ControlMode controlMode = ControlMode.None;
        public ControlMode ControlMode
        {
            get => controlMode;
            set 
            { 
                controlMode = value;
                ControlModeSetting(controlMode);
                onChangeControlMode?.Invoke(controlMode);
            }
        }

        ControlOption controlOption = ControlOption.None;
        public ControlOption ControlOption
        {
            get => controlOption;
            set
            {
                controlOption ^= value;
                onChangeControlOption?.Invoke(controlOption);
            }
        }
        public Action<ControlOption> onChangeControlOption;

        protected override void Init()
        {
            base.Init();

            var optionPanel = panels.Find((panel) => panel.gameObject.name.Equals("OptionPanel"));

            commandHandler = new CommandHandler(optionPanel, 30);

            differnetialIntervalPanel.SetData(ref (parentController as PRSPageController).data);
        }

        private void OnDisable()
        {
            ControlMode = ControlMode.None;
        }

        void ControlModeSetting(ControlMode mode)
        {
            PRSPageController parent = parentController as PRSPageController;

            switch (mode)
            {
                case ControlMode.None:
                    controlPanel.ControlStrategy = null;
                    break;
                case ControlMode.Move:
                    controlPanel.ControlStrategy = new PositionStrategy(commandHandler, parent.data, ControlOption);
                    break;
                case ControlMode.Rotate:
                    controlPanel.ControlStrategy = new RotationStrategy(commandHandler, parent.data, ControlOption);
                    break;
                case ControlMode.Scale:
                    controlPanel.ControlStrategy = new ScaleStrategy(commandHandler, parent.data, ControlOption);
                    break;
                default:
                    break;
            }
        }
    }
}
