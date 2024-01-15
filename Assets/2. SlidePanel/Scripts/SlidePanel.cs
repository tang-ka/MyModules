using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SlidePanel
{
    public class SlidePanel : MonoBehaviour
    {
        #region Members
        [Header("Components")]
        [SerializeField] protected Toggle toggle;
        [SerializeField] protected Transform toggleIcon;
        [SerializeField] protected RectTransform window;
        protected RectTransform rectTransform;

        [Header("Options")]
        [SerializeField] bool isRightStart = true;

        private GraphicRaycaster gr;
        private PointerEventData ped;

        private CancellationTokenSource animationTokenSrc = new CancellationTokenSource();
        private CancellationToken animationToken;

        Vector2 openPosition;
        Vector2 closePotition;
        Vector2 targetPosition;
        #endregion

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {

        }
    }
}
