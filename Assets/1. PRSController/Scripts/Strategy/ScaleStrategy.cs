using UnityEngine;

namespace PRSController
{
    public class ScaleStrategy : ControlStrategy
    {
        public ScaleStrategy(PRSData data, bool isLocal) : base(data, isLocal) { }

        public override void ControlMethod(ControlButton button)
        {
            Vector3 dirVec = Vector3.zero;

            switch (button)
            {
                case ControlButton.X_MINUS:
                    dirVec = Vector3.left;
                    break;

                case ControlButton.X_PLUS:
                    dirVec = Vector3.right;
                    break;

                case ControlButton.Y_MINUS:
                    dirVec = Vector3.down;
                    break;

                case ControlButton.Y_PLUS:
                    dirVec = Vector3.up;
                    break;

                case ControlButton.Z_MINUS:
                    dirVec = Vector3.back;
                    break;

                case ControlButton.Z_PLUS:
                    dirVec = Vector3.forward;
                    break;

                default:
                    break;
            }

            data.TargetObject.transform.localScale += data.DifferentialInterval * dirVec;
        }
    }
}