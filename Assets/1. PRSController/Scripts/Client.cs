using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PRSController;

namespace PRSController
{
    public class Client : MonoBehaviour
    {
        void Start()
        {
            PRSCreator.Instance.Create(new Vector2(0, 20));
        }

    } 
}
