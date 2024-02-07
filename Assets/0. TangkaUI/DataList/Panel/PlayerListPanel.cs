using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class PlayerListPanel : ToggleListPanel<PlayerListItem, PlayerData>
    {
        [SerializeField] SlideToggle tglVisible;
        [SerializeField] RectTransform viewport;

        List<PlayerData> playerList = new List<PlayerData>();

        CancellationTokenSource fadeTokenSource;
        CancellationToken fadeToken;

        public Rect ViewportWorldRect
        {
            get
            {
                var localRect = viewport.rect;

                return new Rect
                {
                    min = viewport.TransformPoint(localRect.min),
                    max = viewport.TransformPoint(localRect.max)
                };
            }
        }

        List<PlayerListItem> itemsOnViewport = new List<PlayerListItem>();

        protected override void Awake()
        {
            base.Awake();

            tglVisible.toggle.onValueChanged.AddListener(ChangeVisible);

            for (int i = 1; i <= 200; i++)
                playerList.Add(new PlayerData(i));

            SetItems(playerList);
        }

        protected override bool ContainItem(PlayerData data)
        {
            return itemList.Find(it => it.Data.nickName.Equals(data.nickName)) != null;
        }

        protected override PlayerListItem Get(PlayerData data)
        {
            return itemList.Find(it => it.Data.nickName.Equals(data.nickName));
        }

        private void ChangeVisible(bool isOn)
        {
            if (fadeToken.CanBeCanceled)
                fadeTokenSource.Cancel();

            fadeTokenSource = new CancellationTokenSource();
            fadeToken = fadeTokenSource.Token;

            SetVisibleItemsOnViewport(!isOn);
        }

        public void CullingMaskedItem()
        {
            itemsOnViewport.Clear();

            for (int i = 0; i < itemList.Count; i++)
            { 
                if (IsOverViewport(itemList[i].WorldRect))
                    itemsOnViewport.Add(itemList[i]);
            }
        }

        private void SetVisibleItemsOnViewport(bool isVisible)
        {
            scrollRect.enabled = isVisible;

            //if (isVisible)
            //    content.gameObject.SetActive(true);

            if (!isVisible)
                CullingMaskedItem();

            //int taskProgress = 0;
            for (int i = 0; i < itemsOnViewport.Count; i++)
            {
                var awaiter = itemsOnViewport[i].SetVisible(isVisible, fadeToken).GetAwaiter();

                //if (!isVisible)
                //{
                //    awaiter.OnCompleted(() =>
                //        {
                //            taskProgress++;
                //            //if (taskProgress == itemsOnViewport.Count)
                //                //content.gameObject.SetActive(false);
                //        });
                //}
            }
        }

        private bool IsOverViewport(Rect rect)
        {
            return ViewportWorldRect.Overlaps(rect);
        } 
    }
}
