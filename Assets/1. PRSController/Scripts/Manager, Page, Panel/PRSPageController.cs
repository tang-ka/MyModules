using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        [SerializeField] Dictionary<string, Page> pageDic = new Dictionary<string, Page>();
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
            foreach (Transform child in transform)
            {
                Page childPage;
                if (child.TryGetComponent<Page>(out childPage))
                {
                    pageDic.Add(child.gameObject.name, childPage);
                }
            }

            isInit = true;
        }

        private void DeInit()
        {
            pageDic.Clear();
            data = null;

            isInit = false;
        }

        private void OpenPage(PageState state, ControlMode mode = ControlMode.None)
        {
            string stateString = state.ToString();

            foreach (var page in pageDic)
            {
                string pageName = page.Key.Replace("Page", "");
                page.Value.gameObject.SetActive(pageName.Equals(stateString));
            }

            //pageDic["ControlPage"].
        }

        public void SetTargetObject(Transform target)
        {
            data.TargetObject = target;
        }
    } 
}
