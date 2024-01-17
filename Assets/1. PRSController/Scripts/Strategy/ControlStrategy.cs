using PRSController;
using UnityEngine;

namespace PRSController
{
    public abstract class ControlStrategy : IControlStrategy
    {
        protected PRSData data;
        //protected bool isLocal;
        protected ControlOption option;
        public ControlOption Option 
        { 
            get => option; 
            set => option = value; 
        }

    public ControlStrategy(PRSData data, ControlOption option)
        {
            this.data = data;
            this.option = option;
        }

        //public bool IsLocal
        //{
        //	get => isLocal;
        //	set => isLocal = value;
        //}

        //public ControlStrategy(PRSData data, bool isLocal)
        //{
        //	this.data = data;
        //	this.isLocal = isLocal;
        //}

        public abstract void ControlMethod(Vector3 dir);
	}
}