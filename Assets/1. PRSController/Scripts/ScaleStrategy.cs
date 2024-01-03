using PRSController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleStrategy : IControlStrategy
{
    Transform target;

    public ScaleStrategy(Transform target)
    {
        this.target = target;
    }

    public void ControlMethod(ControlButton button)
    {
        Debug.Log($"Scale button {button}");
    }
}
