using UnityEngine;

namespace PRSController
{
    public class RotationStrategy : ControlStrategy
    {
        public RotationStrategy(PRSData data, ControlOption option) : base(data, option) { }

        public override void ControlMethod(ControlButton button)
        {
            Vector3 rotVec = Vector3.zero;

            switch (button)
            {
                case ControlButton.X_MINUS:
                    rotVec = Vector3.left;
                    break;

                case ControlButton.X_PLUS:
                    rotVec = Vector3.right;
                    break;

                case ControlButton.Y_MINUS:
                    rotVec = Vector3.down;
                    break;

                case ControlButton.Y_PLUS:
                    rotVec = Vector3.up;
                    break;

                case ControlButton.Z_MINUS:
                    rotVec = Vector3.back;
                    break;

                case ControlButton.Z_PLUS:
                    rotVec = Vector3.forward;
                    break;

                default:
                    break;
            }

#if !UNITY_EDITOR
            if (option.HasFlag(ControlOption.Option_Local))
                data.TargetObject.transform.localRotation *= Quaternion.Euler(data.DifferentialInterval * rotVec);
            else
            {
                data.TargetObject.transform.Rotate(rotVec, data.DifferentialInterval, Space.World);
            }
#else
            if (option.HasFlag(ControlOption.Option_Local))
                data.TargetObject.transform.localRotation *= Quaternion.Euler(5 * rotVec);
            else
            {
                data.TargetObject.transform.Rotate(rotVec, 5, Space.World);
            }
#endif
        }
    }
}
