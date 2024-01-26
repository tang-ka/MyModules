using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TangkaUI
{
    [RequireComponent(typeof(ToggleGroup))]
    public class ToggleListPanel<T, W> : DataListPanel<T, W> where T : ListItem<W>
    {
        protected override bool ContainItem(W data)
        {
            throw new System.NotImplementedException();
        }

        protected override T Get(W data)
        {
            throw new System.NotImplementedException();
        }
    }
}
