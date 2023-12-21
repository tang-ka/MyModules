using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
    public class PRSData
    {
        public Transform TargetObject { get; set; }
        public float DifferentialInterval { get; set; }

        public PRSData(float interval = 0.05f)
        {
            DifferentialInterval = interval;
        }
    }
}