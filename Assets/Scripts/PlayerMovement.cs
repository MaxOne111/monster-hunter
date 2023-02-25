using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour, IMoveable
{
    [SerializeField] private float _Speed;
    private AmmoOnScene _Ammo_On_Scene;
    private PlayerAnimation _Animation;
    public bool IsMove { get; private set; }
    private bool _move_Back;
    private bool _move_Up;

    [Inject]
    private void Construct(AmmoOnScene _ammo)
    {
        _Ammo_On_Scene = _ammo;
    }
    
    private void Awake()
    {
        _Animation = GetComponent<PlayerAnimation>();
        GameEvents._Put_Ammo += Move;
    }

    public void Move()
    {
        if(!IsMove)
            StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        IsMove = true;
        while (_Ammo_On_Scene.AmmoList.Count > 0)
        {
            Vector3 _horizontal = new Vector3(_Ammo_On_Scene.AmmoList[0].transform.position.x, transform.position.y);
            Vector3 _vertical = new Vector3(transform.position.x, _Ammo_On_Scene.AmmoList[0].transform.position.y);
            _Animation.MoveAnimation(IsMove,_move_Back,_move_Up);
            
            if(transform.position != _horizontal)
            {
                transform.position = Vector3.MoveTowards(transform.position, _horizontal, _Speed * Time.deltaTime);
                _Animation.ChangeDirection(_horizontal.x);
                _move_Back = true;
                _move_Up = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _vertical, _Speed * Time.deltaTime);
                _move_Back = false;
                if(transform.position.y < _vertical.y)
                    _move_Up = true;
                else
                {
                    _move_Up = false;
                }
            }
            if (transform.position == _Ammo_On_Scene.AmmoList[0].transform.position)
            {
                Destroy(_Ammo_On_Scene.AmmoList[0]);
                _Ammo_On_Scene.RemoveAmmo(_Ammo_On_Scene.AmmoList[0]);
                GameEvents.TakeAmmo();
            }
            
            yield return null;
        }
        IsMove = false;
        _move_Back = false;
        _move_Up = false;
        _Animation.MoveAnimation(IsMove,_move_Back,_move_Up);
    }

    private void OnDisable()
    {
        GameEvents._Put_Ammo -= Move;
    }

    private void OnDestroy()
    {
        GameEvents._Put_Ammo -= Move;
    }
}
