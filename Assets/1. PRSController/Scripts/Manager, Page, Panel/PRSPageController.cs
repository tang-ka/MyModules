using Mono.Cecil.Cil;
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
        None = -1,
        ModeSelect,
        Contorol
    }

    public class PRSPageController : MonoBehaviour
    {
        #region Members
        [SerializeField] Page[] pages;
        [SerializeField] PRSData data = new PRSData();
        [SerializeField] PageState pageState;
        public PageState PageState 
        { 
            get => pageState; 
            set
            {
                if (value == PageState.None)
                {
                    PRSControllerManager.Instance.Close();
                    return;
                }

                pageState = value;
                OpenPage(value);
            }
        }

        [SerializeField] bool isInit = false;
        #endregion

        #region Mono
        private void Awake()
        {
            Init();
        }

        private void OnDestroy()
        {
            DeInit();
        }

        private void OnEnable()
        {
            OpenSetting();
        }

        private void OnDisable()
        {
            
        }
        #endregion

        public void OpenSetting()
        {
            if (!isInit) 
                Init();

            pageState = PageState.ModeSelect;
        }

        public void CloseSetting()
        {
            data = null;
        }


        // 최초 한번만 설정하면 그 이후에는 설정 할 필요가 없는 설정들
        private void Init()
        {
            pages = new Page[transform.childCount];
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i] = transform.GetChild(i).GetComponent<Page>();
            }

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
