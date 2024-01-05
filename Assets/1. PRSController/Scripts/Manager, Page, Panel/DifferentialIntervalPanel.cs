using PRSController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Lean.Touch;
using static CW.Common.CwInputManager;

public class DifferentialIntervalPanel : MonoBehaviour
{
    PRSData data;

    [SerializeField] Image fill;
    [SerializeField] RectTransform dialHandle;
    [SerializeField] RectTransform handle;
    [SerializeField] Text txtInterval;

    [SerializeField] GraphicRaycaster gRaycaster;
    [SerializeField] PointerEventData eventData;
    [SerializeField] List<RaycastResult> results;

    public float dialValue = 0;
    const float UNIT_VALUE = 0.01f;

    private void Awake()
    {
        var canvas = GetComponentInParent<Canvas>();
        gRaycaster = canvas.GetComponent<GraphicRaycaster>();
        eventData = new PointerEventData(null);
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += DialStart;
        LeanTouch.OnFingerUp += DialFinish;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= DialStart;
        LeanTouch.OnFingerUp -= DialFinish;
    }

    public void SetData(PRSData data)
    {
        this.data = data;
    }

    private void UpdateText()
    {
        txtInterval.text = data.DifferentialInterval.ToString();
    }

    private void DialStart(LeanFinger finger)
    {
        if (!finger.StartedOverGui)
            return;

        if (!GraphicRaycast(finger.StartScreenPosition))
            return;
        
        var layer = results[0].gameObject.layer;
        if (layer == LayerMask.NameToLayer("DialHandle"))
        {
            dialHandlePosition = new Vector2(dialHandle.position.x, dialHandle.position.y);
            startTouchVector = (finger.StartScreenPosition - dialHandlePosition).normalized;
            preTouchVector = startTouchVector;
            LeanTouch.OnFingerUpdate += DialMove;
        }
    }

    Vector2 startTouchVector;
    Vector2 dialHandlePosition;
    Vector2 defaultDialHandlePosition = Vector2.up;

    Vector2 preTouchVector;
    Vector2 curTouchVector;
    float totalDelta;
    private void DialMove(LeanFinger finger)
    {
        curTouchVector = (finger.ScreenPosition - dialHandlePosition).normalized;

        var deltaAngle = SignedAngle(preTouchVector, curTouchVector, -dialHandle.forward);

        totalDelta += deltaAngle;

        if (Mathf.Abs(totalDelta) > 30)
        {
            dialValue += totalDelta/Mathf.Abs(totalDelta) * 30;
            totalDelta -= 30;
        
            dialValue += deltaAngle;
            dialValue = Mathf.Clamp(dialValue, 0, 1079);

            float tempDialValue = dialValue % 360;
            float z = 0;
            if (dialValue <= 180)
                z = Mathf.Floor(-dialValue);
            else
                z = Mathf.Floor(360 - dialValue);

            dialHandle.rotation = Quaternion.Euler(0, 0, z);
            fill.fillAmount = dialValue / 360;
        }

        preTouchVector = curTouchVector;
    }

    private void DialFinish(LeanFinger finger)
    {
        LeanTouch.OnFingerUpdate -= DialMove;
    }

    private bool GraphicRaycast(Vector3 position)
    {
        eventData.position = position;
        results = new List<RaycastResult>();
        gRaycaster.Raycast(eventData, results);

        if (results.Count <= 0)
            return false;
        else
            return true;
    }

    private float SignedAngle(Vector2 from, Vector2 to, Vector3 positiveSide)
    {
        Vector3 fromVec = new Vector3(from.x, from.y, 0);
        Vector3 toVec = new Vector3(to.x, to.y, 0);

        fromVec.Normalize();
        toVec.Normalize();
        positiveSide.Normalize();

        var crossProduct = Vector3.Cross(fromVec, toVec);
        var dotProduct = Vector3.Dot(crossProduct, positiveSide);

        var signedAngle = Vector2.Angle(from, to);

        if (dotProduct > 0)
            return signedAngle;
        else
            return -signedAngle;
    }
}  
