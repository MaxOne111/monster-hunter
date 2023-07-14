using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [field: SerializeField] public GameObject Ammo { get; protected set; }
    [field: SerializeField] public List<WeaponConfig> WeaponLevels { get; private set; }
    public WeaponConfig CurrentWeaponLevel { get=>WeaponLevels[0]; }
    
    public int Level { get=>CurrentWeaponLevel.Level;}
    public float Damage { get=>CurrentWeaponLevel.Damage;}
    public Sprite Icon { get=>CurrentWeaponLevel.Icon;}

    public abstract IEnumerator WeaponAttack(GameObject _target, PlayerShoot _player);
    public abstract void WeaponUIAccept(IWeaponUIVisitor _visitor);
    
}
