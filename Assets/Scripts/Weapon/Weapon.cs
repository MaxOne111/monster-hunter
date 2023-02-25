using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/NewWeapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private float _Damage;
    [SerializeField] private float _Fire_Rate;
    [SerializeField] private GameObject _Bullet_Prefab;
    public float Damage{get=>_Damage;}
    public float FireRate{get=>_Fire_Rate;}
    public GameObject BulletPrefab{get=>_Bullet_Prefab;}
    
}
