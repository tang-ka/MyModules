using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
	public class CommandHandler
	{
		Stack<ControlCommand> controlCommandStack = new Stack<ControlCommand>();
		Stack<ControlCommand> redoCommandStack = new Stack<ControlCommand>();

		public Action onExcuteCommand;

		public void RequestCommand(ControlCommand command)
		{
			DoCommand(command);
            redoCommandStack.Clear();

			onExcuteCommand?.Invoke();
            DebugCommand();
        }

		private void DoCommand(ControlCommand command)
		{
            controlCommandStack.Push(command);
            command.Excute();
        }

		public void UndoCommand()
		{
            redoCommandStack.Push(controlCommandStack.Pop().Undo());

            onExcuteCommand?.Invoke();
            DebugCommand();
        }

		public void RedoCommand()
		{
			DoCommand(redoCommandStack.Pop());

            onExcuteCommand?.Invoke();
            DebugCommand();
        }

		public bool ExistUndoStack()
		{
			return controlCommandStack.Count > 0;
		}

		public bool ExistRedoStack()
		{
            return redoCommandStack.Count > 0;
        }

		public void DebugCommand()
		{
			int i = 1;
			string commandLog = "-----Command Stack-----\n";
			foreach (ControlCommand command in controlCommandStack)
			{

				commandLog += $"Index : \t\t{i}\n" +
							  $"Command : \t{command.GetMyType().Replace("ControlCommand", "")}\n" +
							  $"Direction : \t{command.GetDirection()}\n" +
							  $"Magnituded : \t{command.GetMagnitude()}";

				commandLog += "\n\n";
				i++;
			}

			Debug.Log(commandLog);
		}
	}
}
