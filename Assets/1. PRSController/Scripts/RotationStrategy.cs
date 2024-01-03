using PRSController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationStrategy : IControlStrategy
{
    PRSData data;

    public RotationStrategy(PRSData data)
    {
        this.data = data;
    }

    public void ControlMethod(ControlButton button)
    {
        Vector3 rotVec = Vector3.zero;

        switch (button)
        {
            case ControlButton.X_MINUS:
                rotVec = Vector3.left;
                break;

            case ControlButton.X_PLUS:
                rotVec = Vector3.right;
                break;

            case ControlButton.Y_MINUS:
                rotVec = Vector3.down;
                break;

            case ControlButton.Y_PLUS:
                rotVec = Vector3.up;
                break;

            case ControlButton.Z_MINUS:
                rotVec = Vector3.back;
                break;

            case ControlButton.Z_PLUS:
                rotVec = Vector3.forward;
                break;

            default:
                break;
        }

        data.TargetObject.transform.localRotation *= Quaternion.Euler(5 * rotVec);
    }
}
