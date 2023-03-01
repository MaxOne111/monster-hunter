using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerWeapon))]
public class PlayerShoot : MonoBehaviour, IPlayerAttackable
{
   [field: SerializeField] public Weapon CurrentWeapon { get; set; }
    private PlayerWeapon _Player_Weapon;
    [Header("Bullet Explosion Effect")]
    [SerializeField] private GameObject _Particle_Object;
    [Space]
    [SerializeField] private float _Bullet_Speed;
    private GameUI _Game_UI;

    private GameObject _Explosion_Effect;
    
    private float _Bonus_Damage;

    private int _Attack_Count;
    
    private MonstersPool _Monsters_Pool;
    
    private bool _Is_Combo_Start;
    
    //Combo settings
    [Header("Combo Settings")]
    [SerializeField] private GameObject _Particle_Object_Combo;
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
        
        CurrentWeapon = _Player_Weapon.Weapon[0];
        
        CurrentWeapon.Stats();
        
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
        _Attack_Count++;
        if(!_Is_Combo_Start)
            StartCoroutine(ComboCoroutine());
    }


    private IEnumerator ShootCoroutine()
    {
        Vector3 _bullet_direction = _Monsters_Pool.CurrentMonster.transform.position - transform.position;
        float _angle_Rotate = Mathf.Atan(_bullet_direction.y/_bullet_direction.x) * Mathf.Rad2Deg;
        Quaternion _start_Rotate = Quaternion.Euler(0,0,_angle_Rotate + 90);

        GameObject _bullet = Instantiate(CurrentWeapon.BulletPrefab, transform.position, _start_Rotate);
        while (_Monsters_Pool.CurrentMonster && _bullet.transform.position != _Monsters_Pool.CurrentMonster.transform.position)
        {
            _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, _Monsters_Pool.CurrentMonster.transform.position,
                _Bullet_Speed * Time.deltaTime);
            yield return null;
        }
        if (_Monsters_Pool.CurrentMonster && _Monsters_Pool.CurrentMonster.TryGetComponent(out Monster _monster))
        {
            Combo();
            _monster.ApplyDamage(CurrentWeapon.Damage + _Bonus_Damage);

            _Game_UI.CurrentCombo(_Attack_Count);
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
                StartCombo();
            }
            yield return new WaitForSeconds(1);
        }
        FinishCombo();
    }

    private void StartCombo()
    {
        _Bonus_Damage = CurrentWeapon.Damage;
        _Explosion_Effect = _Particle_Object_Combo;
        _Game_UI.ComboTextSetActive(true);
    }

    private void FinishCombo()
    {
        _Attack_Count = 0;
        _Bonus_Damage = 0;
        _Explosion_Effect = _Particle_Object;
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

