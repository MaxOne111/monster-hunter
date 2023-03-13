using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Ally : MonoBehaviour, IAttackable , IUpgradeable
{
    [SerializeField] private AllyConfig _Config;
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public float Damage { get; private set; }
    private float _Fire_Rate;
    private float _Upgrade_Price;
    private float _Purchase_Price;
    
    [SerializeField] private GameObject _Bullet_Prefab;
    [SerializeField] private float _Bullet_Speed;
    [SerializeField] private ParticleSystem _Explosion_Effect;
    private int _Increase_Damage = 0;
    private float _Next_Fire;
    
    private MonstersPool _Pool;
    private PlayerAllies _Allies;

    [Inject]
    private void Construct(MonstersPool _pool, PlayerAllies _allies)
    {
        _Pool = _pool;
        _Allies = _allies;
        
        Name = _Config.Name;
        Icon = _Config.Icon;
        Damage = _Config.Damage;
        _Fire_Rate = _Config.FireRate;
        _Upgrade_Price = _Config.UpgradePrice;
        _Purchase_Price = _Config.PurchasePrice;
    }

    private void Awake()
    {
        GameEvents._Start_Level += DestroyAlly;
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (_Pool.CurrentMonster && Time.time > _Next_Fire)
        {
            StartCoroutine(Shoot());
            _Next_Fire = 1f / _Fire_Rate + Time.time;
        }
    }

    public void Buy()
    {
        if (_Allies.Allies.Contains(this))
        {
            if (PlayerData.Coins - _Upgrade_Price >= 0)
            {
                Upgrade();
            }
        }
        else
        {
            if (PlayerData.Coins - _Purchase_Price >= 0)
            {
                _Allies.AddAlly(this);
            }
        }
    }

    public void Upgrade()
    {
        Damage += _Config.Damage;
        _Upgrade_Price *= 1.17f;
    }
    
    private IEnumerator Shoot()
    {
        Vector3 _bullet_direction = _Pool.CurrentMonster.transform.position - transform.position;
            float _angle_Rotate = Mathf.Atan(_bullet_direction.y/_bullet_direction.x) * Mathf.Rad2Deg;
            Quaternion _start_Rotate = Quaternion.Euler(0,0,_angle_Rotate + 90);

            GameObject _bullet = Instantiate(_Bullet_Prefab, transform.position, _start_Rotate);
            while (_Pool.CurrentMonster && _bullet.transform.position != _Pool.CurrentMonster.transform.position)
            {
                _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, _Pool.CurrentMonster.transform.position,
                    _Bullet_Speed * Time.deltaTime);
                yield return null;
            }

            float _final_Damage;
            if (_Increase_Damage > 1)
                _final_Damage = Damage * _Increase_Damage;
            else
            {
                _final_Damage = Damage;
            }
        
            if (_Pool.CurrentMonster && _Pool.CurrentMonster.TryGetComponent(out Monster _monster))
            {
                _monster.ApplyDamage(_final_Damage);
            }
            Instantiate(_Explosion_Effect, _bullet.transform.position, Quaternion.identity);
            Destroy(_bullet);
    }

    private void DestroyAlly()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        GameEvents._Start_Level -= DestroyAlly;
    }
}
