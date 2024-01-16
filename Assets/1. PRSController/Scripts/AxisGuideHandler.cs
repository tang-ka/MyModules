using UnityEngine;

namespace PRSController
{
    public class AxisGuideHandler : MonoBehaviour
    {
        [SerializeField] GameObject axisGuidePrefab;
        GameObject axisGuide;
        Transform axisTarget;

        PRSPageController pageController;

        bool isAttached;
        bool isLocal;

        private void Create()
        {
            axisGuide = Instantiate(axisGuidePrefab);

            pageController = GetComponent<PRSPageController>();
            (pageController.pageDic[PageState.Control] as ControlPage).onChangeControlOption += ((ControlOption option) =>
            {
                isLocal = option.HasFlag(ControlOption.Option_Local);
            });
        }

        public void Activate(bool isActive)
        {
            if (axisGuide == null)
                Create();

            axisGuide.SetActive(isActive);
        }

        public void Attach(Transform target)
        {
            axisTarget = target;

            axisGuide.transform.SetParent(axisTarget);
            axisGuide.transform.localPosition = Vector3.zero;

            isAttached = true;
        }

        private void Update()
        {
            if (axisTarget == null || !axisTarget.gameObject.activeInHierarchy)
            {
                isAttached = false;
                return;
            }

            if (!isAttached)
                return;

            if (isLocal)
                axisGuide.transform.localRotation = Quaternion.identity;
            else
                axisGuide.transform.forward = Vector3.forward;
        }
    }
}
