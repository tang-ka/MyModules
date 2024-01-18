using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace PRSController
{
    public class PositionControlCommand : ControlCommand
    {
        public PositionControlCommand(ControlInfo info) : base(info) { }

        public override void Excute()
        {
            Debug.Log("Position control");
        }

        public override ControlCommand Undo()
        {
            Debug.Log("Position control undo");
            return this;
        }
    }

    public class RotationControlCommand : ControlCommand
    {
        public RotationControlCommand(ControlInfo info) : base(info) { }

        public override void Excute()
        {
            Debug.Log("Rotation control");
        }

        public override ControlCommand Undo()
        {
            Debug.Log("Rotation control undo");
            return this;
        }
    }

    public class ScaleControlCommand : ControlCommand
    {
        public ScaleControlCommand(ControlInfo info) : base(info) { }

        public override void Excute()
        {
            Debug.Log("Scale control");
        }

        public override ControlCommand Undo()
        {
            Debug.Log("Scale control undo");
            return this;
        }
    }
}
