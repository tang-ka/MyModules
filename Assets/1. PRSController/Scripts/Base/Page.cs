using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    protected PageController parentController;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init() { }

    public void SetParentController(PageController parentController)
    {
        this.parentController = parentController;
    }
}
