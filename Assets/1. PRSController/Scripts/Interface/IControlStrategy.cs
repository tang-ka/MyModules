using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public interface IControlStrategy
    {
        void ControlMethod(ControlButton button);
    } 
}
