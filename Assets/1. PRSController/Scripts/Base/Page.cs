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
        FindAllChildren(transform);
    }

    void FindAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            FindAllChildren(child);

            Panel panel;
            if (child.TryGetComponent(out panel))
            {
                panels.Add(panel);
            }
        }
    }

    public void SetParentController(PageController parentController)
    {
        this.parentController = parentController;
    }
}
