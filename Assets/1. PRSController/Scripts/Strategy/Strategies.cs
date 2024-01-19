using UnityEngine;

namespace PRSController
{
    public class PositionStrategy : ControlStrategy
    {
        public PositionStrategy(CommandHandler handler, PRSData data, ControlOption option) : base(handler, data, option) { }

        public override void ControlMethod(Vector3 dir)
        {
            commandHandler.RequestCommand(new PositionControlCommand(
                data.TargetObject,
                option,
                dir,
                data.DifferentialInterval
                ));
            
            //if (option.HasFlag(ControlOption.Option_Local))
            //    data.TargetObject.Translate(data.DifferentialInterval * dir, Space.Self);
            //else
            //    data.TargetObject.Translate(data.DifferentialInterval * dir, Space.World);
        }
    }

    public class RotationStrategy : ControlStrategy
    {
        public RotationStrategy(CommandHandler handler, PRSData data, ControlOption option) : base(handler, data, option) { }

        public override void ControlMethod(Vector3 dir)
        {
            commandHandler.RequestCommand(new RotationControlCommand(
                data.TargetObject,
                option,
                dir,
#if !UNITY_EDITOR
                data.DifferentialInterval
#else
                5
#endif
                ));

//#if !UNITY_EDITOR
//            if (option.HasFlag(ControlOption.Option_Local))
//                data.TargetObject.transform.localRotation *= Quaternion.Euler(data.DifferentialInterval * dir);
//            else
//            {
//                data.TargetObject.transform.Rotate(dir, data.DifferentialInterval, Space.World);
//            }
//#else
//            if (option.HasFlag(ControlOption.Option_Local))
//                data.TargetObject.localRotation *= Quaternion.Euler(5 * dir);
//            else
//            {
//                data.TargetObject.Rotate(dir, 5, Space.World);
//            }
//#endif
        }
    }

    public class ScaleStrategy : ControlStrategy
    {
        public ScaleStrategy(CommandHandler handler, PRSData data, ControlOption option) : base(handler, data, option) { }

        public override void ControlMethod(Vector3 dir)
        {
            commandHandler.RequestCommand(new ScaleControlCommand(
                data.TargetObject,
                option,
                dir,
                data.DifferentialInterval
                ));

            //if (option.HasFlag(ControlOption.Option_Constrained))
            //{
            //    bool isPlus = (dir.x > 0) || (dir.y > 0) || (dir.z > 0);
            //    dir = isPlus ? Vector3.one : -Vector3.one;
            //    data.TargetObject.localScale += data.DifferentialInterval * dir;
            //}
            //else
            //    data.TargetObject.localScale += data.DifferentialInterval * dir;
        }
    }
}