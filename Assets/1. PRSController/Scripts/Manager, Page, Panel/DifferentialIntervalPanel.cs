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
            LeanTouch.OnFingerUpdate += DialMove;
        }
    }

    private void DialMove(LeanFinger finger)
    {
        var defaultPosition = Vector2.up;
        //var touchVector = finger.ScreenPosition - dialHandle.position;

        Debug.Log(dialHandle.position);
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

}  
