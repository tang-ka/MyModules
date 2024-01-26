using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
	public class WindowItemDial : MonoBehaviour
	{
		[SerializeField] Dial dial;
        [SerializeField] Toggle tglUseNegative;

        private void Awake()
        {
            tglUseNegative.onValueChanged.AddListener(UserNegative);

            tglUseNegative.isOn = dial.Handle.useNegative; 
        }

        private void UserNegative(bool use)
        {
            dial.Handle.useNegative = use;
        }
    } 
}
