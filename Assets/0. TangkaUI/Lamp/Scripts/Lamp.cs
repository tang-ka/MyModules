using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    public abstract class Lamp : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] protected Image bulb;

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            Monitoring();
        }

        protected virtual void Init()
        {
            bulb = transform.Find("Bulb").GetComponent<Image>();
        }

        protected virtual void Monitoring() { }
    } 
}
