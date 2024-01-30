using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class PlayerListPanel : ToggleListPanel<PlayerListItem, PlayerData>
    {
        [SerializeField] SlideToggle tglVisible;

        List<PlayerData> playerList = new List<PlayerData>();

        CancellationTokenSource fadeTokenSource;
        CancellationToken fadeToken;

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

            if (isOn)
                CloseList();
            else
                OpenList();
        }

        protected override void OpenList()
        {
            for (int i = 0; i < itemList.Count; i++)
                itemList[i].Open(fadeToken);
        }

        protected override void CloseList()
        {
            for (int i = 0; i < itemList.Count; i++)
                itemList[i].Close(fadeToken);
        }
    }
}
