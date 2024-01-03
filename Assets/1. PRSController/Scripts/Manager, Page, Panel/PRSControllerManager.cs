using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PRSController
{
    public class PRSControllerManager : Singleton<PRSControllerManager>
    {
        public GameObject PRSConctrollerPrefab;
        public PRSPageController PRSController { get; private set; }

        // Position 입력하는 방식 좀 더 생각해봐야 함.
        public void Open(Vector2 openPosition)
        {
            if (PRSController == null)
            {
                Create(openPosition);
                return;
            }

            PRSController.GetComponent<RectTransform>().anchoredPosition = openPosition; 
            PRSController.gameObject.SetActive(true);
        }

        public void Open(Vector2 openPosition, Transform target)
        {
            if (PRSController == null)
            {
                Create(openPosition, target);
                return;
            }

            Open(openPosition);
            PRSController.SetTargetObject(target);
        }

        private void Create(Vector2 createPosition)
        {
            PRSController = Instantiate(PRSConctrollerPrefab, transform).GetComponentInParent<PRSPageController>();
            PRSController.GetComponent<RectTransform>().anchoredPosition = createPosition;
        }

        private void Create(Vector2 createPosition, Transform target)
        {
            Create(createPosition);
            PRSController.SetTargetObject(target);
        }

        public void Close()
        {
            PRSController.gameObject.SetActive(false);
        }
    } 
}
