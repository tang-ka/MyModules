using Cysharp.Threading.Tasks;
using Lean.Touch;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SlidePanel
{
    public class SlidePanel : MonoBehaviour
    {
        #region Members
        [Header("Components")]
        [SerializeField] protected Toggle toggle;
        [SerializeField] protected RectTransform toggleIcon;
        [SerializeField] protected RectTransform window;
        protected RectTransform rectTransform;

        [Header("Options")]
        [SerializeField] bool isRightSide = true;

        private GraphicRaycaster gRaycaster;
        private PointerEventData eventData;
        private List<RaycastResult> results;

        private CancellationTokenSource animationTokenSrc = new CancellationTokenSource();
        private CancellationToken animationToken;

        Vector2 openPosition;
        Vector2 closePosition;
        Vector2 targetPosition;
        #endregion

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            OpenSetting();
        }

        private void OnDisable()
        {
            CloseSetting();
        }

        protected virtual void Init()
        {
            toggle.onValueChanged.AddListener(Operate);
            rectTransform = GetComponent<RectTransform>();

            var direction = isRightSide ? -Vector2.right : Vector2.right;
            var moveVec = direction * window.rect.width;
            openPosition = moveVec + rectTransform.anchoredPosition;
            closePosition = rectTransform.anchoredPosition;

            gRaycaster = transform.GetComponentInParent<GraphicRaycaster>();
            eventData = new PointerEventData(null);
        }

        protected virtual void OpenSetting()
        {
            LeanTouch.OnFingerTap += TapOnOutside;
        }

        protected virtual void CloseSetting()
        {
            rectTransform.anchoredPosition = targetPosition;
            toggle.isOn = false;
            LeanTouch.OnFingerTap -= TapOnOutside;
        }

        private void Operate(bool isOpen)
        {
            if (animationTokenSrc != null)
                animationTokenSrc.Cancel();

            animationTokenSrc = new CancellationTokenSource();
            animationToken = animationTokenSrc.Token;

            var goalPosition = isOpen ? openPosition : closePosition;

            SlideMove(goalPosition, animationToken);
            IconChange(isOpen);
        }

        private async void SlideMove(Vector2 goalPosition, CancellationToken slideMoveToken)
        {
            try
            {
                await UniTask.WaitUntil(() =>
                    {
                        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, goalPosition, Time.deltaTime * 10);

                        return Vector2.Distance(rectTransform.anchoredPosition, goalPosition) < 0.05f;
                    }, cancellationToken: slideMoveToken);
                
                rectTransform.anchoredPosition = goalPosition;
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        protected virtual void IconChange(bool isOpen)
        {
            toggleIcon.localScale = isOpen ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1); 
        }

        private void TapOnOutside(LeanFinger finger)
        {
            if (finger.StartedOverGui) return;

            if (finger.Tap)
            {
                if (!GraphicRaycast(finger.LastScreenPosition))
                    toggle.isOn = false;
            }
        }

        private bool GraphicRaycast(Vector2 position)
        {
            eventData.position = position;
            results = new List<RaycastResult>();
            gRaycaster.Raycast(eventData, results);

            if (results.Count <= 0)
                return false;
            else
                return true;
        }


    }
}
