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
