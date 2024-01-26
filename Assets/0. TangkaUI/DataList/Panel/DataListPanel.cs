using PRSController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace TangkaUI
{
    /// <summary>
    /// ex) AnchorListPanel
    /// T : AnchorListItem, W : AnchorData
    /// AnchorListITem : ListItem<AnchorData>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    public abstract class DataListPanel<T, W> : Panel where T : ListItem<W>
    {
        [SerializeField] protected GameObject scrollRect;
        
        [SerializeField] GameObject itemPrefab;
        protected List<T> items = new List<T>();

        protected virtual void SetItems(List<W> datas)
        {
            if (datas == null)
                return;

            for (int i = 0; i < datas.Count; i++)
                AddItem(datas[i]);
        }

        protected virtual void AddItem(W data)
        {
            T item;

            if (ContainItem(data))
                item = Get(data);
            else
            {
                var gameObj = Instantiate(itemPrefab, scrollRect.transform);
                item = gameObj.GetComponent<T>();
                items.Add(item);
            }

            item.Set(data);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
            item.gameObject.SetActive(true);
        }

        protected abstract bool ContainItem(W data);
        protected abstract T Get(W data);

        protected virtual void RemoveAll() { }
        protected virtual void OpenList() { }
        protected virtual void CloseList() { }
    } 
}
