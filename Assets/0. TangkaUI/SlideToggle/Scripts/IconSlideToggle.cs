using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
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

        protected override void ChangeUI(bool isOn)
        {
            base.ChangeUI(isOn);

            icon.color = iconColor[isOn];
            icon.sprite = iconSprite[isOn];
        }

        public override void Interactable(bool isInteractable)
        {
            base.Interactable(isInteractable);

            if (isInteractable)
            {

            }
            else
            {
                icon.color *= new Color(1, 1, 1, 0);
                icon.color += new Color(0, 0, 0, 0.3f);
            }
        }
    }
}

