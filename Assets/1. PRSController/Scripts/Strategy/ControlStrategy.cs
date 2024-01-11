using PRSController;

namespace PRSController
{
	public abstract class ControlStrategy : IControlStrategy
	{
		protected PRSData data;
		protected bool isLocal;
        public bool IsLocal 
		{ 
			get => isLocal;
			set => isLocal = value;
		}

        public ControlStrategy(PRSData data, bool isLocal)
		{
			this.data = data;
			this.isLocal = isLocal;
		}

        public abstract void ControlMethod(ControlButton button);
	}

}