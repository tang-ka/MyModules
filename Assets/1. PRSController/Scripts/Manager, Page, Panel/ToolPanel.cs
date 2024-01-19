using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class ToolPanel : Panel
    {
        [SerializeField] CommandHandler commandHandler;

        [SerializeField] Button btnUndo;
        [SerializeField] Button btnRedo;

        private void Awake()
        {
            ControlPage parentPage;
            if (transform.parent.TryGetComponent(out parentPage))
            {
                commandHandler = parentPage.commandHandler;
                btnUndo.onClick.AddListener(Undo);
                btnRedo.onClick.AddListener(Redo);

                SetInteractable();
                commandHandler.onExcuteCommand += SetInteractable;
            }
        }

        private void Undo()
        {
            commandHandler.UndoCommand();
        }

        private void Redo()
        {
            commandHandler.RedoCommand();
        }

        private void SetInteractable()
        {
            btnUndo.interactable = commandHandler.ExistUndoStack();
            btnRedo.interactable = commandHandler.ExistRedoStack();
        }
    } 
}
