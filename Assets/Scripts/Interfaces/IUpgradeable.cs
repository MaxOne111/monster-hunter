using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable
{
    public int UpgradePrice { get; }
    public void Upgrade();
}
