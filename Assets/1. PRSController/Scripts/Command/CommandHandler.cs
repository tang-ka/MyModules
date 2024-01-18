using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
	public class CommandHandler : MonoBehaviour
	{
		Stack<ControlCommand> controlCommandStack = new Stack<ControlCommand>();
		Stack<ControlCommand> redoCommandStack = new Stack<ControlCommand>();

		public void RequestCommand(ControlCommand command)
		{
			DoCommand(command);
            redoCommandStack.Clear();
		}

		private void DoCommand(ControlCommand command)
		{
            controlCommandStack.Push(command);
            command.Excute();
        }

		public void UndoCommand()
		{
            redoCommandStack.Push(controlCommandStack.Pop().Undo());
		}

		public void RedoCommand()
		{
			DoCommand(redoCommandStack.Pop());
		}
	}
}
