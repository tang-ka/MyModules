using Cysharp.Threading.Tasks;
using Lean.Touch;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlidePanelOld : MonoBehaviour
{
    [SerializeField]
    protected Transform arrow;
    [SerializeField]
    protected Toggle hitBox;
    [SerializeField]
    protected RectTransform window;
    protected RectTransform rectTransform;

    Vector2 openPosition;
    Vector2 closePosition;
    Vector2 targetPosition;

    [SerializeField]
    bool isRightSide = false;
    protected bool onMove = false;

    GraphicRaycaster gRay;
    PointerEventData pointerEvent;

    private CancellationTokenSource tokenSrc = new CancellationTokenSource();
    private CancellationToken openWindowToken;

    protected virtual void Awake()
    {
        int direction;

        if (isRightSide)
            direction = 1;
        else
            direction = -1;

        hitBox.onValueChanged.AddListener(OpenWindow);
        rectTransform = GetComponent<RectTransform>();
        openPosition = new Vector2(direction * (window.rect.width + 5), rectTransform.anchoredPosition.y);
        closePosition = rectTransform.anchoredPosition;

        gRay = GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        pointerEvent = new PointerEventData(EventSystem.current);
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += TouchDownOutside;
    }

    protected virtual void OnDisable()
    {
        rectTransform.anchoredPosition = targetPosition;
        hitBox.isOn = false;
        LeanTouch.OnFingerDown -= TouchDownOutside;
    }

    public async void OpenWindow(bool isOpen)
    {
        onMove = true;

        if (isOpen)
        {
            targetPosition = openPosition;
            arrow.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            targetPosition = closePosition;
            arrow.localScale = new Vector3(1, 1, 1);
        }

        await UniTask.WaitUntil(() =>
        {
            if (rectTransform == null)
                return true;

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * 10);
            return Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < 0.05f;
        }, cancellationToken: openWindowToken);

        if (rectTransform != null)
            rectTransform.anchoredPosition = targetPosition;

        onMove = false;
    }

    private void TouchDownOutside(LeanFinger finger)
    {
        if (finger.StartedOverGui) return;

        List<RaycastResult> results = new List<RaycastResult>();
        pointerEvent.position = finger.StartScreenPosition;

        gRay.Raycast(pointerEvent, results);

        if (results.Count == 0)
            hitBox.isOn = false;
    }
}
