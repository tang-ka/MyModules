using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace PRSController
{
    public class ConnectionLamp : OnOffLamp
    {
        [SerializeField] PRSPageController pageController;
        Transform target;

        protected override void Init()
        {
            base.Init();
            target = pageController.data.TargetObject;
            SetLampColor(Define.CONNECT, Define.DISCONNECT);
        }

        protected override void Monitoring()
        {
            onSwitchLamp?.Invoke(target != null);
        }
    } 
}
