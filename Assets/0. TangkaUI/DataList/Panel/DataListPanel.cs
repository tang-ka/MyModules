using PRSController;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
        [SerializeField] GameObject itemPrefab;
        protected List<T> itemList = new List<T>();

        [SerializeField] protected ScrollRect scrollRect;
        [SerializeField] protected RectTransform content;

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
                var gameObj = Instantiate(itemPrefab, content);
                item = gameObj.GetComponent<T>();
                itemList.Add(item);
            }

            item.Set(data);
            item.GetComponent<RectTransform>().localScale = Vector3.one;
            item.gameObject.SetActive(true);
        }
        
        protected virtual void ClearList() 
        {
            itemList.RemoveAll((item) => item == null);

            for (int i = 0; i < itemList.Count; i++)
                Destroy(itemList[i].gameObject);
        }

        protected virtual void OpenList() 
        {
            content.gameObject.SetActive(true);
        }

        protected virtual void CloseList() 
        {
            content.gameObject.SetActive(false);
        }

        protected abstract bool ContainItem(W data);
        protected abstract T Get(W data);
    } 
}
