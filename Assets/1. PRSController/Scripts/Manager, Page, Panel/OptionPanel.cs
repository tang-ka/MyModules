using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
    public class OptionPanel : Panel
    {
        [SerializeField] SlideToggle tglGlobalLocal;
        [SerializeField] SlideToggle tglConstrain; 
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
            tglGlobalLocal.Interactable(mode == ControlMode.Move || mode == ControlMode.Rotate);
            tglConstrain.Interactable(mode == ControlMode.Scale);
        }

        private void IsConstrained(bool isContrained)
        {
            parentPage.ControlOption = ControlOption.Option_Constrained;
            //parentPage.isContrained = isContrained;
        }

        private void IsLocal(bool isLocal)
        {
            parentPage.ControlOption = ControlOption.Option_Local;
            //parentPage.IsLocal = isLocal;
        }
    } 
}
