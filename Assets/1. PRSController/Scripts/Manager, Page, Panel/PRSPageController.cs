using System;
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
        Control
    }

    public class PRSPageController : PageController
    {
        #region Members
        [SerializeField]
        public Dictionary<PageState, Page> pageDic = new Dictionary<PageState, Page>();

        [SerializeField]
        public PRSData data = new PRSData();

        [SerializeField]
        PageState pageState;
        public PageState PageState
        {
            get => pageState;
            set { pageState = value; }
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
            CloseSetting();
        }
        #endregion

        public void OpenSetting()
        {
            if (!isInit)
                Init();

            OpenPage(PageState.ModeSelect);
        }

        public void CloseSetting()
        {
            data = new PRSData();
        }

        // 최초 한번만 설정하면 그 이후에는 설정 할 필요가 없는 설정들
        private void Init()
        {
            foreach (Transform child in transform)
            {
                Page childPage;
                if (child.TryGetComponent(out childPage))
                {
                    string nameToState = childPage.gameObject.name.Replace("Page", "");

                    PageState result = PageState.None;

                    if (Enum.TryParse(nameToState, out result))
                    {
                        pageDic.Add(result, childPage);
                        childPage.SetParentController(this);
                    }
                    else
                    {
                        Debug.Log(nameToState);
                        Debug.LogError($"게임 오브젝트 {child.gameObject.name} 의 이름을 'PageState + Page'로 변경해주세요.");
                    }
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

        public void OpenPage(PageState state, ControlMode mode = ControlMode.None)
        {
            if (mode != ControlMode.None)
            {
                (pageDic[PageState.Control] as ControlPage).ControlMode = mode;
            }

            PageState = state;

            foreach (var page in pageDic)
            {
                page.Value.gameObject.SetActive(page.Key.Equals(state));
            }
        }

        public void SetTargetObject(Transform target)
        {
            data.TargetObject = target;
        }
    }
}
