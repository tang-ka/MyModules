using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
	public class CommandHandler
	{
		OptionPanel optionPanel;
		int maxCapacity;

		Stack<ControlCommand> controlCommandStack;
		Stack<ControlCommand> redoCommandStack;

		public Action onExcuteCommand;

        public CommandHandler(Panel panel, int capacity = 15)
        {
            optionPanel = panel as OptionPanel;
            maxCapacity = capacity;

			controlCommandStack = new Stack<ControlCommand>(maxCapacity);
			redoCommandStack = new Stack<ControlCommand>(maxCapacity);
        }

        public void RequestCommand(ControlCommand command)
		{
			DoCommand(command);
            redoCommandStack.Clear();

			onExcuteCommand?.Invoke();
            DebugCommand();
        }

		private void DoCommand(ControlCommand command)
		{
			AdjustCapacity();

            controlCommandStack.Push(command);
            command.Excute();
        }

		public void UndoCommand()
		{
			var undoCommand = controlCommandStack.Pop();
			CombackStatusAtCommand(undoCommand);

            redoCommandStack.Push(undoCommand.Undo());

            onExcuteCommand?.Invoke();
            DebugCommand();
        }

		public void RedoCommand()
		{
            var redoCommand = redoCommandStack.Pop();
			CombackStatusAtCommand(redoCommand);

            DoCommand(redoCommand);

            onExcuteCommand?.Invoke();
            DebugCommand();
        }

		private void CombackStatusAtCommand(ControlCommand command)
		{
			PRSControllerManager.Instance.Refresh(command.GetTarget(), false);
			optionPanel.tglGlobalLocal.toggle.isOn = command.GetOption().HasFlag(ControlOption.Option_Local);
            optionPanel.tglConstrain.toggle.isOn = command.GetOption().HasFlag(ControlOption.Option_Constrained);
        }

        private void AdjustCapacity()
		{
			if (controlCommandStack.Count >= maxCapacity)
			{
				var tempStack = controlCommandStack.ToArray();
				tempStack[tempStack.Length - 1] = null;

				controlCommandStack.Clear();
				for (int i = tempStack.Length - 2; i >= 0; i--)
				{
					controlCommandStack.Push(tempStack[i]);
				}
			}
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
							  $"Target : \t\t{command.GetTarget().name}\n" +
							  $"Command : \t{command.GetMyType().Replace("ControlCommand", "")}\n" +
							  $"Direction : \t{command.GetDirection()}\n" +
							  $"Magnituded : \t{command.GetMagnitude()}";

				commandLog += "\n\n";
				i++;
			}

			int j = 1;
            commandLog += "------Redo Stack------\n";
            foreach (ControlCommand command in redoCommandStack)
            {
                commandLog += $"Index : \t\t{j}\n" +
                              $"Target : \t\t{command.GetTarget().name}\n" +
                              $"Command : \t{command.GetMyType().Replace("ControlCommand", "")}\n" +
                              $"Direction : \t{command.GetDirection()}\n" +
                              $"Magnituded : \t{command.GetMagnitude()}";

                commandLog += "\n\n";
                j++;
            }

            Debug.Log(commandLog);
		}
	}
}
