using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
    public class PositionControlCommand : ControlCommand
    {
        public PositionControlCommand(Transform tf, ControlOption co, Vector3 dir, float mag)
            : base(tf, co, dir, mag) { }

        public override void Excute()
        {
            if (option.HasFlag(ControlOption.Option_Local))
                target.Translate(magnitude * direction, Space.Self);
            else
                target.Translate(magnitude * direction, Space.World);

            //Debug.Log($"Position control\n{option}\t\t{direction}");
        }

        public override ControlCommand Undo()
        {
            if (option.HasFlag(ControlOption.Option_Local))
                target.Translate(-magnitude * direction, Space.Self);
            else
                target.Translate(-magnitude * direction, Space.World);

            //Debug.Log($"Position control undo\n{option}\t\t{direction}");
            return this;
        }

        //public override string GetMyType()
        //{
        //    return this.GetType().Name;
        //}
    }

    public class RotationControlCommand : ControlCommand
    {
        public RotationControlCommand(Transform tf, ControlOption co, Vector3 dir, float mag)
            : base(tf, co, dir, mag) { }

        public override void Excute()
        {
            if (option.HasFlag(ControlOption.Option_Local))
                target.localRotation *= Quaternion.Euler(magnitude * direction);
            else
                target.Rotate(direction, magnitude, Space.World);

            //Debug.Log($"Rotation control\n{option}\t\t{direction}");
        }

        public override ControlCommand Undo()
        {
            if (option.HasFlag(ControlOption.Option_Local))
                target.localRotation *= Quaternion.Euler(-magnitude * direction);
            else
                target.Rotate(direction, -magnitude, Space.World);

            //Debug.Log($"Rotation control undo\n{option}\t\t{direction}");
            return this;
        }
    }

    public class ScaleControlCommand : ControlCommand
    {
        public ScaleControlCommand(Transform tf, ControlOption co, Vector3 dir, float mag)
            : base(tf, co, dir, mag) { }

        public override void Excute()
        {
            if (option.HasFlag(ControlOption.Option_Constrained))
            {
                bool isPlus = (direction.x > 0) || (direction.y > 0) || (direction.z > 0);
                direction = isPlus ? Vector3.one : -Vector3.one;
                target.localScale += magnitude * direction;
            }
            else
                target.localScale += magnitude * direction;

            //Debug.Log($"Scale control\n{option}\t\t{direction}");
        }

        public override ControlCommand Undo()
        {
            if (option.HasFlag(ControlOption.Option_Constrained))
            {
                bool isPlus = (direction.x > 0) || (direction.y > 0) || (direction.z > 0);
                direction = isPlus ? Vector3.one : -Vector3.one;
                target.localScale += -magnitude * direction;
            }
            else
                target.localScale += -magnitude * direction;

            //Debug.Log($"Scale control undo\n{option}\t\t{direction}");
            return this;
        }
    }
}
