using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class PositionStrategy : IControlStrategy
    {
        PRSData data;

        public PositionStrategy(PRSData data)
        {
            this.data = data;
        }

        public void ControlMethod(ControlButton button)
        {
            Vector3 dirVec = Vector3.zero;

            switch (button)
            {
                case ControlButton.X_MINUS:
                    dirVec = Vector3.left;
                    break;

                case ControlButton.X_PLUS:
                    dirVec = Vector3.right;
                    break;
                
                case ControlButton.Y_MINUS:
                    dirVec = Vector3.down;
                    break;
                
                case ControlButton.Y_PLUS:
                    dirVec = Vector3.up;
                    break;
                
                case ControlButton.Z_MINUS:
                    dirVec = Vector3.back;
                    break;
                
                case ControlButton.Z_PLUS:
                    dirVec = Vector3.forward;
                    break;
                
                default:
                    break;
            }

            data.TargetObject.transform.Translate(data.DifferentialInterval * dirVec, Space.Self);
        }
    }
}