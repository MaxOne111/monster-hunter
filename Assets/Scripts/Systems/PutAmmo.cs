using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class PutAmmo : MonoBehaviour
{
    private AmmoOnScene _Ammo_On_Scene;
    private PlayerWeapon _Player_Weapon;
    private Transform _Player;

    private Camera _Camera;
    

    [Inject]
    private void Construct(AmmoOnScene _ammo, PlayerWeapon _player_Weapon)
    {
        _Ammo_On_Scene = _ammo;
        _Player_Weapon = _player_Weapon;
    }

    private void Awake()
    {
        _Camera = Camera.main;
        _Player = _Player_Weapon.transform;
    }

    private void Update()
    {
        Put();
    }


    private void Put()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 _ray = _Camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D _hit = Physics2D.Raycast(_ray, Vector2.zero);

            if (_hit.collider)
            {
                if (_hit.collider.CompareTag("Walkable") && _hit.collider.transform.position != _Player.position)
                {
                    GameObject _ammo = Instantiate(_Player_Weapon.CurrentWeapon.Ammo, _hit.collider.transform.position, Quaternion.identity);
                    _Ammo_On_Scene.AddAmmo(_ammo);
                    _Ammo_On_Scene.NearestAmmo(_Player.gameObject);
                    GameEvents.PutAmmo();
                }
            }
        }
    }
}
