using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class PutAmmo : MonoBehaviour
{
    [SerializeField] private GameObject _Ammo;
    
    [SerializeField] private Tilemap _Walkable;
    
    private AmmoOnScene _Ammo_On_Scene;

    private Camera _Camera;

    private PlayerShoot _Player_Shoot;

    private float _Next_Ammo;

    [Inject]
    private void Construct(AmmoOnScene _ammo)
    {
        _Ammo_On_Scene = _ammo;
    }

    private void Awake()
    {
        _Player_Shoot = GetComponent<PlayerShoot>();
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

            Vector3Int _cell = _Walkable.WorldToCell(_ray);
            
            Vector3 _position = _Walkable.GetCellCenterLocal(_cell);

            if (_hit.collider)
            {
                if (_hit.collider.CompareTag("Walkable"))
                {
                    if (Time.time > _Next_Ammo)
                    {
                        _Next_Ammo = Time.time + 1f / _Player_Shoot.PlayerWeapon.FireRate;
                        GameObject _ammo = Instantiate(_Ammo, _position, Quaternion.identity);
                        _Ammo_On_Scene.AddAmmo(_ammo);
                        _Ammo_On_Scene.NearestAmmo(gameObject);
                        GameEvents.PutAmmo();
                    }
                    
                }
            }
        }
    }
}
