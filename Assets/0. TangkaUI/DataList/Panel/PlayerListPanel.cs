using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class PlayerListPanel : ToggleListPanel<PlayerListItem, PlayerData>
    {
        [SerializeField] SlideToggle tglVisible;

        List<PlayerData> playerList = new List<PlayerData>();

        protected override void Awake()
        {
            base.Awake();

            tglVisible.toggle.onValueChanged.AddListener(ChangeVisible);

            for (int i = 1; i <= 15; i++)
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
            if (isOn)
                CloseList();
            else
                OpenList();
        }

        protected override void OpenList()
        {
            base.OpenList();

            for (int i = 0; i < itemList.Count; i++)
            {
                FadeIn(itemList[i].gameObject);
            }


        }

        protected override void CloseList()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                FadeOut(itemList[i].gameObject, base.CloseList);
            }
        }

        List<float> defaultAlpha = new List<float>();

        async void FadeOut(GameObject obj, Action onClose = null)
        {
            var texts = obj.GetComponentsInChildren<Text>();
            var images = obj.GetComponentsInChildren<Image>();

            float testAlpha = 1;
            float damperRatio = 0.9f;

            await UniTask.WaitUntil(() =>
            {
                testAlpha *= damperRatio;
                for (int i = 0; i < texts.Length; i++)
                {
                    var txtClr = texts[i].color;

                    //if (i == 0)
                    //    defaultAlpha.Add(txtClr.a);

                    texts[i].color = new Color(txtClr.r, txtClr.g, txtClr.b, txtClr.a * damperRatio);
                }

                for (int i = 0; i < images.Length; i++)
                { 
                    var imgClr = images[i].color;

                    //if (i == 0)
                    //    defaultAlpha.Add(imgClr.a);

                    images[i].color = new Color(imgClr.r, imgClr.g, imgClr.b, imgClr.a * damperRatio);
                }

                if (testAlpha < 0.01f)
                {
                    onClose?.Invoke();
                    return true;
                }

                return false;
            });
        }

        async void FadeIn(GameObject obj, Action onOpen = null)
        {
            var texts = obj.GetComponentsInChildren<Text>();
            var images = obj.GetComponentsInChildren<Image>();

            float increaseRatio = 1.09f;

            await UniTask.WaitUntil(() =>
            {
                bool isAllFinish = true;
                for (int i = 0; i < texts.Length; i++)
                {
                    var txtClr = texts[i].color;
                    texts[i].color = new Color(txtClr.r, txtClr.g, txtClr.b, txtClr.a * increaseRatio);

                    //var isFinish = txtClr.a > defaultAlpha[i];

                    if (isAllFinish &= (txtClr.a > defaultAlpha[i]))
                    {
                        texts[i].color = new Color(txtClr.r, txtClr.g, txtClr.b, defaultAlpha[i]);
                    }
                }

                for (int i = 0; i < images.Length; i++)
                {
                    var imgClr = images[i].color;
                    images[i].color = new Color(imgClr.r, imgClr.g, imgClr.b, imgClr.a * increaseRatio);

                    if (isAllFinish &= (imgClr.a > defaultAlpha[i + texts.Length]))
                    {
                        images[i].color = new Color(imgClr.r, imgClr.g, imgClr.b, defaultAlpha[i + texts.Length]);
                    }
                }

                onOpen?.Invoke();

                return isAllFinish;
            });
        }
    }
}
