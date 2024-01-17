using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class ControlCommand : ICommand
    {
        public class ControlInfo
        {
            public  Transform        target;
            public  ControlStrategy  strategy;
            public  ControlOption    option;
            public  Vector3          direction;
            public  float            magnitude;

            public ControlInfo(Transform tf, ControlStrategy cs, ControlOption co, Vector3 dir, float mag)
            {
                target = tf;
                strategy = cs;
                option = co;
                direction = dir;
                magnitude = mag;
            }
        }

        ControlInfo info;

        public ControlCommand(ControlInfo info)
        {
            this.info = info;
        }

        public void Excute()
        {
            throw new System.NotImplementedException();
        }
    } 
}
