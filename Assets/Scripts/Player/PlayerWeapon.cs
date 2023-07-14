using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerShoot))]
public class PlayerWeapon : MonoBehaviour
{
    [field: SerializeField] public Weapon CurrentWeapon { get; private set; }
    [field: SerializeField] public List<Weapon> Weapon { get; private set; }
    
    public void ChangeWeapon(Weapon _weapon)
    {
        CurrentWeapon = _weapon;
    }
}
