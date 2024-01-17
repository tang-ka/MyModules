using UnityEngine;

namespace PRSController
{
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
                data.TargetObject.transform.localRotation *= Quaternion.Euler(5 * dir);
            else
            {
                data.TargetObject.transform.Rotate(dir, 5, Space.World);
            }
#endif
        }
    }
}
