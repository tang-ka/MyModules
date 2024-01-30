using PRSController;
using UnityEngine;

namespace PRSController
{
    public abstract class ControlStrategy : IControlStrategy
    {
        protected CommandHandler commandHandler;
        protected PRSData data;
        //protected bool isLocal;
        protected ControlOption option;
        public ControlOption Option 
        { 
            get => option; 
            set => option = value; 
        }

        public ControlStrategy(CommandHandler handler, PRSData data, ControlOption option)
        {
            commandHandler = handler;
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