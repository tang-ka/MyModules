using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public abstract class ControlCommand : ICommand
    {
        //public class ControlInfo
        //{
        //    public  Transform        target;
        //    public  ControlOption    option;
        //    public  Vector3          direction;
        //    public  float            magnitude;

        //    public ControlInfo(Transform tf, ControlOption co, Vector3 dir, float mag)
        //    {
        //        target = tf;
        //        option = co;
        //        direction = dir;
        //        magnitude = mag;
        //    }
        //}

        //protected ControlInfo info;

        //public ControlCommand(ControlInfo info)
        //{
        //    this.info = info;
        //}

        protected Transform target;
        protected ControlOption option;
        protected Vector3 direction;
        protected float magnitude;

        public ControlCommand(Transform tf, ControlOption co, Vector3 dir, float mag)
        {
            target = tf;
            option = co;
            direction = dir;
            magnitude = mag;
        }

        public abstract void Excute();
        public abstract ControlCommand Undo();

        //public abstract string GetMyType();

        public virtual string GetMyType()
        {
            return this.GetType().Name;
        }

        public virtual string GetMagnitude()
        {
            return magnitude.ToString();
        }

        public virtual string GetDirection()
        {
            return direction.ToString();
        }
    } 
}
