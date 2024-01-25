using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;

namespace TangkaUI
{
    public class DialHandle : MonoBehaviour
    {
        [SerializeField] int intervalAngle = 30;
        public int IntervalAngle
        {
            get => intervalAngle;
        }

        [SerializeField] int handleValue = 0;
        public int HandleValue
        {
            get => handleValue;
            set
            {
                if (!useNegative)
                {
                    handleValue = Math.Clamp(value, 0, int.MaxValue);
                    receiveHandleValue?.Invoke(handleValue + 1);
                }
                else
                {
                    handleValue = Math.Clamp(value, -int.MaxValue, int.MaxValue);
                    receiveHandleValue?.Invoke(handleValue);
                }
            }
        }

        public Action<int> receiveHandleValue;

        [SerializeField] bool useNegative = false;

        RectTransform rectTransform;
        LayerMask handleLayer;

        GraphicRaycaster gRaycaster;
        PointerEventData eventData;
        List<RaycastResult> results;

        Vector2 handlePosition;
        Vector2 startTouchVector;
        Vector2 preTouchVector;
        Vector3 curTouchVector;

        [SerializeField] float totalDeltaAngle;
        bool isRotated = false;

        float damper = 0.5f;
        const float DAMPER_RATIO = 0.9f;
        
        public void Init(string layerName, bool useNegative = false)
        {
            handleLayer = LayerMask.NameToLayer(layerName);
            this.useNegative = useNegative;

            rectTransform = transform.GetComponent<RectTransform>();

            var canvas = GetComponentInParent<Canvas>();
            gRaycaster = canvas.GetComponent<GraphicRaycaster>();
            eventData = new PointerEventData(null);
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += DialStart;
            LeanTouch.OnFingerUp += DialFinish;

            HandleValue = 0;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= DialStart;
            LeanTouch.OnFingerUp -= DialFinish;
        }

        private void DialStart(LeanFinger finger)
        {
            if (!finger.StartedOverGui)
                return;

            if (!GraphicRaycast(finger.StartScreenPosition))
                return;

            var layer = results[0].gameObject.layer;
            if (layer == handleLayer)
            {
                handlePosition = new Vector2(rectTransform.position.x, rectTransform.position.y);
                startTouchVector = (finger.StartScreenPosition - handlePosition).normalized;
                preTouchVector = startTouchVector;
                LeanTouch.OnFingerUpdate += DialMove;
            }
        }

        public void DialMove(LeanFinger finger)
        {
            curTouchVector = (finger.ScreenPosition - handlePosition).normalized;

            var deltaAngle = SignedAngle(preTouchVector, curTouchVector, -rectTransform.forward);
            totalDeltaAngle += deltaAngle;

            if (deltaAngle != 0)
            {
                rectTransform.rotation *= Quaternion.Euler(0, 0, -deltaAngle * damper);
                damper *= DAMPER_RATIO;
                damper = Math.Clamp(damper, 0.15f, 1f);
            }

            if (Mathf.Abs(totalDeltaAngle) > intervalAngle)
            {
                if (totalDeltaAngle > 0)
                {
                    HandleValue++;
                    totalDeltaAngle -= intervalAngle;
                }
                else
                {
                    HandleValue--;
                    totalDeltaAngle += intervalAngle;
                }

                RotateHandle(handleValue * intervalAngle);
                damper = 1;
                isRotated = true;
            }
            else
            {
                isRotated = false;
            }

            preTouchVector = curTouchVector;
        }

        private void DialFinish(LeanFinger finger)
        {
            LeanTouch.OnFingerUpdate -= DialMove;
            if (!isRotated)
            {
                RotateHandle(handleValue * intervalAngle);
                damper = 1;
                totalDeltaAngle = 0;
            }
        }

        private void RotateHandle(float rotateAngle)
        {
            rotateAngle %= 360;

            if (rotateAngle <= 180)
                rotateAngle = Mathf.Floor(-rotateAngle);
            else
                rotateAngle = Mathf.Floor(360 - rotateAngle);

            rectTransform.rotation = Quaternion.Euler(0, 0, rotateAngle);
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

        private float SignedAngle(Vector2 from, Vector2 to, Vector3 positiveAxis)
        {
            Vector3 fromVec = new Vector3(from.x, from.y, 0);
            Vector3 toVec = new Vector3(to.x, to.y, 0);

            fromVec.Normalize();
            toVec.Normalize();
            positiveAxis.Normalize();

            var crossProduct = Vector3.Cross(fromVec, toVec);
            var dotProduct = Vector3.Dot(crossProduct, positiveAxis);

            var signedAngle = Vector2.Angle(from, to);

            if (dotProduct > 0)
                return signedAngle;
            else
                return -signedAngle;
        }
    }
} 
	
