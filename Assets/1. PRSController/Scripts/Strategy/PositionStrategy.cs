using UnityEngine;

namespace PRSController
{
    public class PositionStrategy : ControlStrategy
    {
        public PositionStrategy(PRSData data, ControlOption option) : base(data, option) { }

        public override void ControlMethod(Vector3 dir)
        {
            if (option.HasFlag(ControlOption.Option_Local))
                data.TargetObject.Translate(data.DifferentialInterval * dir, Space.Self);
            else
                data.TargetObject.Translate(data.DifferentialInterval * dir, Space.World);
        }
    }
}