using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerShoot))]
public class PlayerWeapon : MonoBehaviour
{
    private PlayerShoot _Player_Shoot;
    private GameObject _Ammo;
    [field: SerializeField] public List<Weapon> Weapon { get; private set; }

    private void Awake()
    {
        _Player_Shoot = GetComponent<PlayerShoot>();
    }

    public void ChangeWeapon(Weapon _new_Weapon)
    {
        _Player_Shoot.CurrentWeapon = _new_Weapon;
        _new_Weapon.Stats();
    }
}
