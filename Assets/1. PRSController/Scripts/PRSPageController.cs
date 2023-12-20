using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PRSController
{
    /// <summary>
    /// 1. 하위 Page를 알아서 알고 있고 싶다.
    /// 2. 하위 Page의 On/Off를 책임지고 싶다.
    /// </summary>
    
    public enum PageState
    {
        ModeSelect,
        Contorol
    }

    public class PRSPageController : MonoBehaviour
    {
        #region Members
        [SerializeField]
        Page[] pages;

        PRSData data;

        PageState pageState;

        bool isInit = false;
        #endregion

        #region Mono
        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }
        #endregion

        public void Open()
        {
            if (!isInit) Init();

            
        }

        public void Close()
        {

        }

        private void Init()
        {
            pages = new Page[transform.childCount];
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i] = transform.GetChild(i).GetComponent<Page>();
            }

            data.DifferentialInterval = 0.05f;

            isInit = true;
        }

        private void DeInit()
        {
            pages = null;
            data = null;

            isInit = false;
        }

        public void OpenPage(PageState state)
        {
            string stateString = state.ToString();

            for (int i = 0; i < pages.Length; i++)
            {
                string pageString = pages[i].gameObject.name.Replace("Page", "");
                pages[i].gameObject.SetActive(pageString.Equals(stateString));
            }
        }

        public void SetTargetObject(Transform target)
        {
            data.TargetObject = target;
        }
    } 
}
