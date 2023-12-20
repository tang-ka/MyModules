using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PRSController
{
    public class PRSCreator : Singleton<PRSCreator>
    {
        public GameObject PRSConctrollerPrefab;
        public GameObject PRSController { get; private set; }

        // Position 입력하는 방식 좀 더 생각해봐야 함.
        public void Create(Vector2 createPosition)
        {
            if (PRSController != null) return;

            PRSController = Instantiate(PRSConctrollerPrefab, transform);
            PRSController.GetComponent<RectTransform>().anchoredPosition = createPosition;
        }
    } 
}
