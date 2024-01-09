using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRSController
{
    public abstract class Lamp : MonoBehaviour
    {
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
