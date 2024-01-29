using System;
using System.Collections;
using System.Collections.Generic;
using TangkaUI;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

namespace PRSController
{
    public class OptionPanel : Panel
    {
        [SerializeField] public SlideToggle tglGlobalLocal;
        [SerializeField] public SlideToggle tglConstrain; 
        ControlPage parentPage;

        private void Awake()
        {
            tglGlobalLocal.toggle.onValueChanged.AddListener(IsLocal);
            tglConstrain.toggle.onValueChanged.AddListener(IsConstrained);

            parentPage = GetComponentInParent<ControlPage>();

            parentPage.onChangeControlMode += SetInteractableOption;
        }

        private void Start()
        {
            SetInteractableOption(parentPage.ControlMode);
        }

        private void SetInteractableOption(ControlMode mode)
        {
            tglGlobalLocal.toggle.interactable = (mode == ControlMode.Move || mode == ControlMode.Rotate);
            tglGlobalLocal.ChangeUI(tglGlobalLocal.toggle.isOn);

            tglConstrain.toggle.interactable = mode == ControlMode.Scale;
            tglConstrain.ChangeUI(tglConstrain.toggle.isOn);
        }

        private void IsLocal(bool isLocal)
        {
            parentPage.ControlOption = ControlOption.Option_Local;
        }

        private void IsConstrained(bool isContrained)
        {
            parentPage.ControlOption = ControlOption.Option_Constrained;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                tglGlobalLocal.toggle.isOn = !tglGlobalLocal.toggle.isOn;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                tglConstrain.toggle.isOn = !tglConstrain.toggle.isOn;
            }
        }
    } 
}
