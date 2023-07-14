using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerWeapon))]
public class PlayerShoot : MonoBehaviour, IAttackable
{
    private PlayerWeapon _Player_Weapon;
    private GameUI _Game_UI;
    private MonstersPool _Monsters_Pool;

    private float _Combo_Damage;

    private int _Attack_Count;

    private bool _Is_Combo_Start;
    
    public float IncreaseDamage { get; set; }
    
    //Combo settings
   // [Header("Combo Settings")]
    [field: SerializeField] public float ResetTime { get; set; }
    [field: SerializeField] public int StartComboAttack { get; set; }
    //

    [Inject]
    private void Construct(MonstersPool _monsters_Pool, GameUI _ui)
    {
        _Monsters_Pool = _monsters_Pool;
        _Game_UI = _ui;
    }
    private void Awake()
    {
        _Player_Weapon = GetComponent<PlayerWeapon>();
        GameEvents._Take_Ammo += Attack;
        //CurrentWeapon.Stats();
    }

    public void Attack()
    {
        if (_Monsters_Pool.CurrentTarget)
        {
            StartCoroutine(_Player_Weapon.CurrentWeapon.WeaponAttack(_Monsters_Pool.CurrentTarget, this));
        }
    }

    // public void Combo()
    // {
    //     _Attack_Count++;
    //     if(!_Is_Combo_Start)
    //         StartCoroutine(ComboCoroutine());
    // }
    

    private IEnumerator ComboCoroutine()
    {
        _Is_Combo_Start = true;
        
        float _current_Time = ResetTime;
        
        while (_current_Time > 0)
        {
            _current_Time--;
            if (_Attack_Count >= StartComboAttack)
            {
                StartCombo();
            }
            yield return new WaitForSeconds(1);
        }
        FinishCombo();
    }

    private void StartCombo()
    {
        _Combo_Damage = _Player_Weapon.CurrentWeapon.Damage;
        //_Explosion_Effect = _Particle_Object_Combo;
        _Game_UI.ComboTextSetActive(true);
    }

    private void FinishCombo()
    {
        _Attack_Count = 0;
        _Combo_Damage = 0;
        //_Explosion_Effect = _Particle_Object;
        _Game_UI.ComboTextSetActive(false);
        _Is_Combo_Start = false;
    }
    
    private void OnDisable()
    {
        GameEvents._Take_Ammo -= Attack;
    }

    private void OnDestroy()
    {
        GameEvents._Take_Ammo -= Attack;
    }
    
}

