using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public abstract class ControlCommand : ICommand
    {
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

        public virtual Transform GetTarget()
        {
            return target;
        }

        public virtual ControlOption GetOption()
        {
            return option;
        }

        public virtual float GetMagnitude()
        {
            return magnitude;
        }

        public virtual Vector3 GetDirection()
        {
            return direction;
        }
    } 
}
