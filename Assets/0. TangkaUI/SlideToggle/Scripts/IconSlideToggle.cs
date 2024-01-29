using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public class IconSlideToggle : SlideToggle
    {
        [SerializeField] Image icon;

        [Header("Source Images")]
        [SerializeField] Sprite onImg;
        [SerializeField] Sprite offImg;

        Dictionary<bool, Sprite> iconSprite = new Dictionary<bool, Sprite>();
        Dictionary<bool, Color> iconColor = new Dictionary<bool, Color>();

        Vector2 iconOffPosition;

        CancellationTokenSource iconMoveTokenSource;
        CancellationToken iconMoveToken;

        protected override void Awake()
        {
            SetIconOn(onImg, Define.THEME_BLACK);
            SetIconOff(offImg, Define.THEME_WHITE);

            iconOffPosition = icon.GetComponent<RectTransform>().anchoredPosition;

            base.Awake();
        }

        protected override void Animate(bool isOn)
        {
            if (iconMoveTokenSource != null)
                iconMoveTokenSource.Cancel();

            iconMoveTokenSource = new CancellationTokenSource();
            iconMoveToken = iconMoveTokenSource.Token;

            var iconTargetPosition = iconOffPosition * translateVector[isOn];

            SlideMove(icon.GetComponent<RectTransform>(), iconTargetPosition, iconMoveToken);

            base.Animate(isOn);
        }

        protected void SetIconOn(Sprite iconOnSprite, Color iconOnColor)
        {
            iconSprite.Add(true, iconOnSprite);
            iconColor.Add(true, iconOnColor);
        }

        protected void SetIconOff(Sprite iconOffSprite, Color iconOffColor)
        {
            iconSprite.Add(false, iconOffSprite);
            iconColor.Add(false, iconOffColor);
        }

        public override void ChangeUI(bool isOn)
        {
            icon.color = iconColor[isOn];
            icon.sprite = iconSprite[isOn];

            base.ChangeUI(isOn);
        }

        public override void Interactable(bool isInteractable)
        {
            if (!isInteractable)
            { 
                icon.color *= new Color(1, 1, 1, 0);
                icon.color += new Color(0, 0, 0, 0.3f);
            }

            base.Interactable(isInteractable);
        }
    }
}

