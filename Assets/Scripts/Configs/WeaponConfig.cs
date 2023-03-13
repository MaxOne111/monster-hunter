using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Configs/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private int _Level;
    [SerializeField] private float _Damage;
    [SerializeField] private int _Upgrade_Price;
    
    [SerializeField] private GameObject _Bullet_Prefab;
    [SerializeField] private Sprite _Icon;
    
    public int Level{get=>_Level;}
    public float Damage{get=>_Damage;}
    public int UpgradePrice{get=>_Upgrade_Price;}
    public GameObject BulletPrefab{get=>_Bullet_Prefab;}
    public Sprite Icon{get=>_Icon;}
    
}
