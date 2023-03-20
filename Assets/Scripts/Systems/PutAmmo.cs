using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class PutAmmo : MonoBehaviour
{
    private AmmoOnScene _Ammo_On_Scene;
    private PlayerShoot _Player_Shoot;

    private Camera _Camera;
    

    [Inject]
    private void Construct(AmmoOnScene _ammo, PlayerShoot _player_Shoot)
    {
        _Ammo_On_Scene = _ammo;
        _Player_Shoot = _player_Shoot;
    }

    private void Awake()
    {
        _Camera = Camera.main;
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
                if (_hit.collider.CompareTag("Walkable") && _hit.collider.transform.position != _Player_Shoot.transform.position)
                {
                    GameObject _ammo = Instantiate(_Player_Shoot.CurrentWeapon.Ammo, _hit.collider.transform.position, Quaternion.identity);
                    _Ammo_On_Scene.AddAmmo(_ammo);
                    _Ammo_On_Scene.NearestAmmo(_Player_Shoot.gameObject);
                    GameEvents.PutAmmo();
                }
            }
        }
    }
}
