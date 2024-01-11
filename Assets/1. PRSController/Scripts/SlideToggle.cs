using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public class SlideToggle : Toggle
    {
        [SerializeField] Image background;
        [SerializeField] Image handle;
        [SerializeField] Text textMark;

        Dictionary<bool, Color> backgroundColor = new Dictionary<bool, Color>();
        Dictionary<bool, Color> handleColor = new Dictionary<bool, Color>();
        Dictionary<bool, Color> textMarkColor = new Dictionary<bool, Color>();

        Vector2 handlePosition;
        Vector2 textMarkPosition;

        protected override void Awake()
        {
            base.Awake();

            background = GetComponent<Image>();
            handle = transform.Find("Handle").GetComponent<Image>();
            textMark = GetComponentInChildren<Text>();

            SetOnColor(Define.THEME_ALPHAWHITE, Define.THEME_BLACK, Define.THEME_BLACK);
            SetOffColor(Define.THEME_ALPHABLACK, Define.THEME_WHITE, Define.THEME_WHITE);

            handlePosition = handle.GetComponent<RectTransform>().anchoredPosition;
            textMarkPosition = textMark.GetComponent<RectTransform>().anchoredPosition;

            Animate(isOn);
            onValueChanged.AddListener(Animate);
        }

        private void Animate(bool isOn)
        {
            if (isOn)
            {
                textMark.text = "L";
                Debug.Log("Animate toggle on");
            }
            else
            {
                textMark.text = "W";
                Debug.Log("Animate toggle off");
            }

            ChangeColor(isOn);
        }

        void SlideMove()
        {
            //handle.GetComponent<RectTransform>().anchoredPosition;
        }

        void SetOnColor(Color bgON, Color hdlON, Color txtON)
        {
            backgroundColor[true] = bgON;
            handleColor[true] = hdlON;
            textMarkColor[true] = txtON;
        }

        void SetOffColor(Color bgOFF, Color hdlOFF, Color txtOFF)
        {
            backgroundColor[false] = bgOFF;
            handleColor[false] = hdlOFF;
            textMarkColor[false] = txtOFF;
        }

        void ChangeColor(bool isOn)
        {
            background.color = backgroundColor[isOn];
            handle.color = handleColor[isOn];
            textMark.color = textMarkColor[isOn];
        }
    }
}
