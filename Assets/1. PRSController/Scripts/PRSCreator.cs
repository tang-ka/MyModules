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

        // Position �Է��ϴ� ��� �� �� �����غ��� ��.
        public void Create(Vector2 createPosition)
        {
            if (PRSController != null) return;

            PRSController = Instantiate(PRSConctrollerPrefab, transform);
            PRSController.GetComponent<RectTransform>().anchoredPosition = createPosition;
        }
    } 
}
