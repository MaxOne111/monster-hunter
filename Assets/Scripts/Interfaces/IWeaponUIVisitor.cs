using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponUIVisitor
{
    public IUpgradeable UpgradeableWeapon { set; }
    public void ShowWeaponData(Weapon _weapon);
    public void ShowWeaponData(Weapon _weapon, IUpgradeable _upgradeable);
}
