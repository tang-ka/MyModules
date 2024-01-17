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

    public class RotationStrategy : ControlStrategy
    {
        public RotationStrategy(PRSData data, ControlOption option) : base(data, option) { }

        public override void ControlMethod(Vector3 dir)
        {

#if !UNITY_EDITOR
            if (option.HasFlag(ControlOption.Option_Local))
                data.TargetObject.transform.localRotation *= Quaternion.Euler(data.DifferentialInterval * dir);
            else
            {
                data.TargetObject.transform.Rotate(dir, data.DifferentialInterval, Space.World);
            }
#else
            if (option.HasFlag(ControlOption.Option_Local))
                data.TargetObject.localRotation *= Quaternion.Euler(5 * dir);
            else
            {
                data.TargetObject.Rotate(dir, 5, Space.World);
            }
#endif
        }
    }

    public class ScaleStrategy : ControlStrategy
    {
        public ScaleStrategy(PRSData data, ControlOption option) : base(data, option) { }

        public override void ControlMethod(Vector3 dir)
        {
            if (option.HasFlag(ControlOption.Option_Constrained))
            {
                bool isPlus = (dir.x > 0) || (dir.y > 0) || (dir.z > 0);
                dir = isPlus ? Vector3.one : -Vector3.one;
                data.TargetObject.localScale += data.DifferentialInterval * dir;
            }
            else
                data.TargetObject.localScale += data.DifferentialInterval * dir;
        }
    }
}