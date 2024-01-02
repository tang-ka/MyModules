using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PRSController
{
    /// <summary>
    /// 1. ���� Page�� �˾Ƽ� �˰� �ְ� �ʹ�.
    /// 2. ���� Page�� On/Off�� å������ �ʹ�.
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
        [SerializeField] Dictionary<PageState, Page> pageDic = new Dictionary<PageState, Page>();
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

        // ���� �ѹ��� �����ϸ� �� ���Ŀ��� ���� �� �ʿ䰡 ���� ������
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
                        Debug.LogError($"���� ������Ʈ {child.gameObject.name} �� �̸��� 'PageState + Page'�� �������ּ���.");
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
            string stateStr = state.ToString();

            foreach (var page in pageDic)
            {
                page.Value.gameObject.SetActive(page.Key.Equals(state));
            }

            if (mode != ControlMode.None)
            {
                (pageDic[PageState.Control] as ControlPage).ControlMode = mode;
            }
        }

        public void SetTargetObject(Transform target)
        {
            data.TargetObject = target;
        }
    } 
}
