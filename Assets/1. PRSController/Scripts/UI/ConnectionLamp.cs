using System.Collections;
using System.Collections.Generic;
using TangkaUI;
using UnityEngine;

namespace PRSController
{
    public class ConnectionLamp : OnOffLamp
    {
        [Header("Data")]
        [SerializeField] PRSPageController pageController;
        Transform target;

        protected override void Init()
        {
            base.Init();

            pageController.onSetTarget += ((target) => this.target = target);

            SetLampColor(Define.CONNECT, Define.DISCONNECT);
        }

        protected override void Monitoring()
        {
            onSwitchLamp?.Invoke(target != null && target.gameObject.activeInHierarchy);
        }
    } 
}
