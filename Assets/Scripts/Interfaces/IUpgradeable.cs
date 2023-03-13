using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable : IBuyable
{
    public void Upgrade();
}
