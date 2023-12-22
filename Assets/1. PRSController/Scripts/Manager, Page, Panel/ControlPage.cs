using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private ControlMode controlMode;

        public ControlMode ControlMode
        {
            get => controlMode;
            set
            { 
                controlMode = value; 
            }

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
