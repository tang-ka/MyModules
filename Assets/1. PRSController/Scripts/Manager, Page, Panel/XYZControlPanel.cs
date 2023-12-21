using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class XYZControlPanel : MonoBehaviour
    {
        [SerializeField] Button[] controlButtons;

        IControlStrategy controlStrategy;
        public IControlStrategy ControlStrategy
        {
            get => controlStrategy;
            set
            {
                controlStrategy = value;
                controlStrategy.SetButtons(ref controlButtons);
            }
        }


        private void Awake()
        {
            controlButtons = GetComponentsInChildren<Button>();
        }


    }
}
