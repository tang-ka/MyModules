using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public interface IControlStrategy
    {
        void SetButtons(ref Button[] buttons);
        void ControlMethod(int index);
    } 
}
