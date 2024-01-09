using PRSController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Page : MonoBehaviour
{
    protected PageController parentController;

    public List<Panel> panels = new List<Panel>();

    private void Awake() { Init(); }

    protected virtual void Init() 
    { 
        
    }

    List<T> FindInChildren<T>(Transform parent)
    {
        List<T> result = new List<T>();

        foreach (transform child in parent)
        {
            child.
        }
    }

    public void SetParentController(PageController parentController)
    {
        this.parentController = parentController;
    }
}
