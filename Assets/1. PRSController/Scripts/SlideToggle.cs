using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class SlideToggle : Toggle
    {
        [SerializeField] Image background;
        [SerializeField] Image handle;
        [SerializeField] Text txtIcon;
        [SerializeField] Image imgIcon;

        Dictionary<bool, Color> backgroundColor = new Dictionary<bool, Color>();
        Dictionary<bool, Color> handleColor = new Dictionary<bool, Color>();
        Dictionary<bool, Color> iconColor = new Dictionary<bool, Color>();

        Vector2 handleOffPosition;
        Vector2 iconOffPosition;

        bool isImgIcon = false;

        private CancellationTokenSource cancellationTokenSource;

        protected override void Awake()
        {
            base.Awake();

            background = GetComponent<Image>();
            handle = transform.Find("Handle").GetComponent<Image>();

            if (transform.Find("Icon").TryGetComponent(out imgIcon))
            {
                isImgIcon = true;
            }
            else
            {
                txtIcon = GetComponentInChildren<Text>();
                isImgIcon = false;
            }

            SetOnColor(Define.THEME_ALPHAWHITE, Define.THEME_BLACK, Define.THEME_BLACK);
            SetOffColor(Define.THEME_ALPHABLACK, Define.THEME_WHITE, Define.THEME_WHITE);

            isOn = false;

            handleOffPosition = handle.GetComponent<RectTransform>().anchoredPosition;
            var rectTransform = isImgIcon ? imgIcon.GetComponent<RectTransform>() : txtIcon.GetComponent<RectTransform>();
            iconOffPosition = rectTransform.anchoredPosition;

            onValueChanged.AddListener(Animate);
            Animate(isOn);
        }

        private void Animate(bool isOn)
        {
            if (cancellationTokenSource != null)
                cancellationTokenSource.Cancel();

            cancellationTokenSource = new CancellationTokenSource();

            var changeFactor = isOn ? Vector2.left : Vector2.right;
            changeFactor += Vector2.up;

            var handleTargetPosition = handleOffPosition * changeFactor;
            var iconTargetPosition = iconOffPosition * changeFactor;

            var iconRectTransform = isImgIcon ? imgIcon.GetComponent<RectTransform>() : txtIcon.GetComponent<RectTransform>();

            SlideMove(handle.GetComponent<RectTransform>(), handleTargetPosition, cancellationTokenSource.Token);
            SlideMove(iconRectTransform, iconTargetPosition, cancellationTokenSource.Token);

            if (!isImgIcon)
                txtIcon.text = isOn ? "L" : "W";

            ChangeColor(isOn);
        }

        async void SlideMove(RectTransform entity, Vector2 targetPosition, CancellationToken cancellationToken)
        {
            
            await UniTask.WaitUntil(() =>
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    entity.anchoredPosition = Vector2.Lerp(entity.anchoredPosition, targetPosition, Time.deltaTime * 10);

                    if (Vector2.Distance(entity.anchoredPosition, targetPosition) < 0.05f)
                    {
                        entity.anchoredPosition = targetPosition;

                        return true;
                    }
                }
                catch (OperationCanceledException)
                {
                    return true;
                }

                return false;
            });

        }

        void SetOnColor(Color bgON, Color hdlON, Color txtON)
        {
            backgroundColor[true] = bgON;
            handleColor[true] = hdlON;
            iconColor[true] = txtON;
        }

        void SetOffColor(Color bgOFF, Color hdlOFF, Color txtOFF)
        {
            backgroundColor[false] = bgOFF;
            handleColor[false] = hdlOFF;
            iconColor[false] = txtOFF;
        }

        void ChangeColor(bool isOn)
        {
            background.color = backgroundColor[isOn];
            handle.color = handleColor[isOn];

            if (isImgIcon)
                imgIcon.color = iconColor[isOn];
            else
                txtIcon.color = iconColor[isOn];
        }
    }
}
