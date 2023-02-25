using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class PlayerShoot : MonoBehaviour, IPlayerAttackable
{
    [SerializeField] private Weapon _Weapon;
    [Header("Bullet Explosion Effect")]
    [SerializeField] private GameObject _Particle_Object;
    [Space]
    [SerializeField] private float _Bullet_Speed;

    private GameUI _Game_UI;

    private GameObject _Explosion_Effect;
    
    private float _Damage;

    private int _Attack_Count;
    
    private MonstersPool _Monsters_Pool;
    
    private bool _Is_Combo_Start;
    
    //Combo settings
    [Header("Combo Settings")]
    [SerializeField] private GameObject _Particle_Object_Combo;
    [field: SerializeField] public float ResetTime { get; set; }
    [field: SerializeField] public int StartComboAttack { get; set; }
    //

    public Weapon PlayerWeapon{get=>_Weapon;}

    [Inject]
    private void Construct(MonstersPool _monsters_Pool, GameUI _ui)
    {
        _Monsters_Pool = _monsters_Pool;
        _Game_UI = _ui;
    }
    private void Awake()
    {
        GameEvents._Take_Ammo += Attack;
        _Damage = _Weapon.Damage;
        _Explosion_Effect = _Particle_Object;
    }
    
    public void Attack()
    {
        if (_Monsters_Pool.CurrentMonster)
        {
            StartCoroutine(ShootCoroutine());
        }
    }

    public void Combo()
    {
        if(!_Is_Combo_Start)
            StartCoroutine(ComboCoroutine());
    }


    private IEnumerator ShootCoroutine()
    {
        Vector3 _bullet_direction = _Monsters_Pool.CurrentMonster.transform.position - transform.position;
        float _angle_Rotate = Mathf.Atan(_bullet_direction.y/_bullet_direction.x) * Mathf.Rad2Deg;
        Quaternion _start_Rotate = Quaternion.Euler(0,0,_angle_Rotate + 90);
        
        GameObject _bullet = Instantiate(_Weapon.BulletPrefab, transform.position, _start_Rotate);
        
        while (_bullet.transform.position != _Monsters_Pool.CurrentMonster.transform.position)
        {
            _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, _Monsters_Pool.CurrentMonster.transform.position,
                _Bullet_Speed * Time.deltaTime);
            yield return null;
        }
        if (_Monsters_Pool.CurrentMonster.TryGetComponent(out Monsters _monster))
        {
            _Attack_Count++;
            Combo();
            _monster.ApplyDamage(_Damage);
        }
        Instantiate(_Explosion_Effect, _bullet.transform.position, Quaternion.identity);
        Destroy(_bullet);
    }

    private IEnumerator ComboCoroutine()
    {
        _Is_Combo_Start = true;
        
        float _current_Time = ResetTime;
        
        while (_current_Time > 0)
        {
            _current_Time--;
            if (_Attack_Count >= StartComboAttack)
            {
                StartCombo(_current_Time);
            }
            yield return new WaitForSeconds(1);
        }
        FinishCombo();
    }

    private void StartCombo(float _current_Time)
    {
        _Damage = _Weapon.Damage * 2;
        _Explosion_Effect = _Particle_Object_Combo;
        _Game_UI.ComboTimeSetActive(true);
        _Game_UI.CurrentComboTime(ResetTime, _current_Time);
    }

    private void FinishCombo()
    {
        _Attack_Count = 0;
        _Damage = _Weapon.Damage;
        _Explosion_Effect = _Particle_Object;
        _Game_UI.ComboTimeSetActive(false);
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
