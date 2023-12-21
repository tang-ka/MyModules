using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
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


        // ���� �ѹ��� �����ϸ� �� ���Ŀ��� ���� �� �ʿ䰡 ���� ������
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
