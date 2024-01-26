using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class ToggleListItem<T> : ListItem<T>
    {
        protected Toggle toggle;
        [SerializeField] Button btnDelete;

        protected virtual void Start()
        {
            toggle = GetComponent<Toggle>();
            toggle.group = transform.GetComponentInParent<ToggleGroup>();
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(isSelected =>
            {
                if (isSelected)
                    SelectItem();
                else
                    DeselectItem();
            });

            LinkDeleteButtonListener();
        }

        protected void LinkDeleteButtonListener()
        {
            if (btnDelete == null)
                return;

            btnDelete.onClick.RemoveAllListeners();
            btnDelete.onClick.AddListener(DeleteItem);
        }

        protected override void SelectItem() 
        {
            Debug.Log($"Select {gameObject.name}");
        }

        protected override void DeselectItem()
        {
            Debug.Log($"Deselect {gameObject.name}");
        }

        protected virtual void DeleteItem() { }
    } 
}
