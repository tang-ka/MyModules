using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PRSController;
using Lean.Touch;
using System;

namespace PRSController
{
    public class TestClient : MonoBehaviour
    {
        private void Awake()
        {
            LeanTouch.OnFingerOld += ObjectControlStart ;
        }

        private void ObjectControlStart(LeanFinger finger)
        {
            Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition); ;
            RaycastHit hit;
            int controlableLayer = 1 << LayerMask.NameToLayer("PRSControlableObject");

            if (Physics.Raycast(ray, out hit, 50f, controlableLayer))
            {
                PRSControllerManager.Instance.Open(new Vector2(0, 20), hit.transform.root);
            }
        }

        private void OnDestroy()
        {
            LeanTouch.OnFingerOld -= ObjectControlStart ;
        }
    } 
}
