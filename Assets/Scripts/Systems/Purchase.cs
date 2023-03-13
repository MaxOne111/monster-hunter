using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    public void Buy(IBuyable _item)
    {
        _item.Buy();
    }
}
