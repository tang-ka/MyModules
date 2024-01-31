using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class PlayerListItem : ToggleListItem<PlayerData>
    {
        [Header("Information")]
        [SerializeField] Text nickName;
        [SerializeField] Text level;
        [SerializeField] Text job;
        [Header("Stat")]
        [SerializeField] Text STR;
        [SerializeField] Text DEX;
        [SerializeField] Text INT;
        [SerializeField] Text LUK;

        [Header("Color(alpha) Value")]
        [SerializeField] List<Text> texts = new List<Text>();
        [SerializeField] List<Image> images = new List<Image>();
        [SerializeField] List<Color> txtDefaultColor = new List<Color>();
        [SerializeField] List<Color> imgDefaultAlpha = new List<Color>();
        float fadeOutSpeed = 25;
        float fadeInSpeed = 18;

        //Dictionary<Text, Color> textDic = new Dictionary<Text, Color>();
        //Dictionary<Image, Color> imageDic = new Dictionary<Image, Color>();

        private void Awake()
        {
            texts = GetComponentsInChildren<Text>().ToList();
            images = GetComponentsInChildren<Image>().ToList();

            for (int i = 0; i < texts.Count; i++)
                txtDefaultColor.Add(texts[i].color);

            for (int i = 0; i < images.Count; i++)
                imgDefaultAlpha.Add(images[i].color);

            //for (int i = 0; i < texts.Length; i++)
            //    textDic.Add(texts[i], texts[i].color);

            //for (int i = 0; i < images.Length; i++)
            //    imageDic.Add(images[i], images[i].color);
        }

        public override void Set(PlayerData data)
        {
            base.Set(data);

            gameObject.name = data.nickName;
            nickName.text = data.nickName;
            level.text = data.level.ToString();

            var jobString = data.job.ToString();
            job.text = jobString[0].ToString();
            job.text += jobString[1..].ToLower();

            STR.text = data.STR.ToString();
            DEX.text = data.DEX.ToString();
            INT.text = data.INT.ToString();
            LUK.text = data.LUK.ToString();
        }

        public void Open(CancellationToken token = default)
        {
            FadeIn(token).Forget();
        }

        public void Close(CancellationToken token = default)
        {
            FadeOut(token).Forget();
        }

        public async UniTaskVoid FadeOut(CancellationToken token = default)
        {
            await UniTask.WaitUntil(() =>
                {
                    for (int i = 0; i < texts.Count; i++)
                    {
                        var txtClr = texts[i].color;
                        var closeClr = new Vector4(txtClr.r, txtClr.g, txtClr.b, 0);

                        texts[i].color = Vector4.Lerp(txtClr, closeClr, Time.deltaTime * fadeOutSpeed);
                    }

                    for (int i = 0; i < images.Count; i++)
                    {
                        var imgClr = images[i].color;
                        var closeClr = new Vector4(imgClr.r, imgClr.g, imgClr.b, 0);

                        images[i].color = Vector4.Lerp(imgClr, closeClr, Time.deltaTime * fadeOutSpeed);
                    }

                    if (texts[0].color.a < 0.001f)
                    {
                        gameObject.SetActive(false);
                        return true;
                    }

                    return false;
                }, cancellationToken: token);
        }

        public async UniTaskVoid FadeIn(CancellationToken token = default)
        {
            gameObject.SetActive(true);

            await UniTask.WaitUntil(() =>
                {
                    bool isAllFinish = true;

                    for (int i = 0; i < texts.Count; i++)
                    {
                        var txtClr = texts[i].color;
                        var openClr = txtDefaultColor[i];

                        bool isFinish = MathF.Abs(txtClr.a - openClr.a) < 0.1f;
                        isAllFinish &= isFinish;

                        if (!isFinish)
                            texts[i].color = Vector4.Lerp(txtClr, openClr, Time.deltaTime * fadeInSpeed);
                        else
                            texts[i].color = openClr;
                    }

                    for (int i = 0; i < images.Count; i++)
                    {
                        var imgClr = images[i].color;
                        var openClr = imgDefaultAlpha[i];

                        bool isFinish = MathF.Abs(imgClr.a - openClr.a) < 0.1f;
                        isAllFinish &= isFinish;

                        if (!isFinish)
                            images[i].color = Vector4.Lerp(imgClr, openClr, Time.deltaTime * fadeInSpeed);
                        else
                            images[i].color = openClr;
                    }

                    if (isAllFinish)
                        return true;

                    return false;
                }, cancellationToken: token);
        }
    }
}

