using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class PositionStrategy : MonoBehaviour, IControlStrategy
    {
        public void SetButtons(ref Button[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;
                buttons[index].onClick.AddListener(() => ControlMethod(index));
            }
        }

        public void ControlMethod(int index)
        {
            Debug.Log($"Pushed button-{index}");
        }
    }
}