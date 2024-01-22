using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    [RequireComponent(typeof(Toggle))]
    public class SlideToggle : MonoBehaviour
    {
        [Header("Components")]
        public Toggle toggle;
        [SerializeField] Image background;
        [SerializeField] Image handle;

        Dictionary<bool, Color> backgroundColor = new Dictionary<bool, Color>();
        Dictionary<bool, Color> handleColor = new Dictionary<bool, Color>();

        protected Dictionary<bool, Vector2> translateVector = new Dictionary<bool, Vector2>()
        {
            { true, new Vector2(-1, 1) },
            { false, new Vector2(1, 1) },
        };

        Vector2 handleOffPosition;

        CancellationTokenSource handleMoveTokenSource;
        CancellationToken handleMoveToken;

        protected virtual void Awake()
        {
            toggle = GetComponent<Toggle>();
            background = GetComponent<Image>();

            SetOnColor(Define.THEME_ALPHAWHITE, Define.THEME_BLACK);
            SetOffColor(Define.THEME_ALPHABLACK, Define.THEME_WHITE);

            toggle.isOn = false;

            handleOffPosition = handle.GetComponent<RectTransform>().anchoredPosition;

            toggle.onValueChanged.AddListener(Animate);
            Animate(toggle.isOn);
        }

        protected virtual void Animate(bool isOn)
        {
            if (handleMoveTokenSource != null)
                handleMoveTokenSource.Cancel();

            handleMoveTokenSource = new CancellationTokenSource();
            handleMoveToken = handleMoveTokenSource.Token;

            var handleTargetPosition = handleOffPosition * translateVector[isOn];

            SlideMove(handle.GetComponent<RectTransform>(), handleTargetPosition, handleMoveToken);

            ChangeUI(isOn);
        }

        protected async void SlideMove(RectTransform entity, Vector2 targetPosition, CancellationToken cancellationToken)
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

        protected void SetOnColor(Color bgON, Color hdlON)
        {
            backgroundColor[true] = bgON;
            handleColor[true] = hdlON;
        }

        protected void SetOffColor(Color bgOFF, Color hdlOFF)
        {
            backgroundColor[false] = bgOFF;
            handleColor[false] = hdlOFF;
        }

        public virtual void ChangeUI(bool isOn)
        {
            background.color = backgroundColor[isOn];
            handle.color = handleColor[isOn];

            Interactable(toggle.IsInteractable());
        }

        public virtual void Interactable(bool isInteractable)
        {
            if (!isInteractable)
            {
                background.color *= new Color(1, 1, 1, 0);
                background.color += new Color(0, 0, 0, 0.3f);
                handle.color *= new Color(1, 1, 1, 0);
                handle.color += new Color(0, 0, 0, 0.3f);
            }
        }
    }
}
