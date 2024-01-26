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
        }
    }
}