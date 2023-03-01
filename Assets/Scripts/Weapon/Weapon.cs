using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IUpgradeable
{
    [field: SerializeField] public GameObject Ammo { get; private set; }
    [field: SerializeField] public List<WeaponConfig> Upgrades { get; private set; }
    public int Level { get; private set; }
    public float Damage { get; private set; }
    public int UpgradePrice { get; private set; }
    public GameObject BulletPrefab { get; private set; }
    public Sprite Icon { get; private set; }
    
    public void Stats()
    {
        Level = Upgrades[0].Level;
        Damage = Upgrades[0].Damage;
        UpgradePrice = Upgrades[0].UpgradePrice;
        BulletPrefab = Upgrades[0].BulletPrefab;
        Icon = Upgrades[0].Icon;
    }
    
    public void Upgrade()
    {
        Upgrades.Remove(Upgrades[0]);
        Stats();
    }

    public void Buy()
    {
        if (PlayerData.SpendCoins(UpgradePrice) && Upgrades.Count > 1)
        {
            Upgrade();
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("Failed");
        }
    }
}
