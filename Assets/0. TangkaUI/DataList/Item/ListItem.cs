using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TangkaUI
{
    public abstract class ListItem<T> : MonoBehaviour
    {
        protected T data;

        public T Data => data;

        public bool IsSelected { get; protected set; }

        protected abstract void SelectItem();
        protected abstract void DeselectItem();

        public virtual void Set(T data)
        {
            this.data = data;
        }
    } 
}
