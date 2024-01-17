using UnityEngine;

namespace PRSController
{
    public class ScaleStrategy : ControlStrategy
    {
        public ScaleStrategy(PRSData data, ControlOption option) : base(data, option) { }

        public override void ControlMethod(Vector3 dir)
        {
            if (option.HasFlag(ControlOption.Option_Constrained))
            {
                bool isPlus = (dir.x > 0) || (dir.y > 0) || (dir.z > 0);
                dir = isPlus ? Vector3.one : -Vector3.one;
                data.TargetObject.transform.localScale += data.DifferentialInterval * dir;
            }
            else
                data.TargetObject.transform.localScale += data.DifferentialInterval * dir;
        }
    }
}