using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Ally : MonoBehaviour, IAttackable, IBuyable, IUpgradeable
{
    [SerializeField] private AllyConfig _Current_Config;
    [SerializeField] private AllyConfig _Start_Config;

    private AllyConfig _Config;
    public string Name { get=>_Current_Config.Name; private set=>_Current_Config.Name=value; }
    public Sprite Icon { get=>_Current_Config.Icon; private set=>_Current_Config.Icon=value; }
    public int Level { get=>_Current_Config.Level; private set=>_Current_Config.Level=value; }
    public float Damage { get=>_Current_Config.Damage; private set=>_Current_Config.Damage=value; }
    public float FireRate { get=>_Current_Config.FireRate; private set=>_Current_Config.FireRate=value; }
    public int UpgradePrice { get=>_Current_Config.UpgradePrice; set=>_Current_Config.UpgradePrice=value;}
    public int PurchasePrice { get=>_Current_Config.PurchasePrice; private set=>_Current_Config.PurchasePrice=value; }
    
    [SerializeField] private GameObject _Bullet_Prefab;
    [SerializeField] private float _Bullet_Speed;
    [SerializeField] private ParticleSystem _Explosion_Effect;
    
    private int _Increase_Damage = 0;
    private float _Next_Fire;
    
    private MonstersPool _Monster_Pool;
    private PlayerAllies _Allies;

    [Inject]
    private void Construct(MonstersPool _pool, PlayerAllies _allies)
    {
        _Monster_Pool = _pool;
        _Allies = _allies;
    }

    private void SetStats()
    {
        _Config = _Start_Config ? _Start_Config : _Current_Config;
        
        Name = _Config.Name;
        Icon = _Config.Icon;
        Level = _Config.Level;
        Damage = _Config.Damage;
        FireRate = _Config.FireRate;
        UpgradePrice = _Config.UpgradePrice;
        PurchasePrice = _Config.PurchasePrice;
    }

    private void Awake()
    {
        GameEvents._Start_Level += DestroyAlly;
        SetStats();
    }

    private void Update()
    {
        Attack();
    }
    
    public void Buy()
    {
        if (!_Allies.Allies.Contains(this))
        {
            if (PlayerData.SpendCoins(PurchasePrice))
            {
                _Allies.AddAlly(this);
            }
        }
    }

    public void Upgrade()
    {
        if (PlayerData.SpendCoins(UpgradePrice))
        {
            _Current_Config.Damage += 2.5f;
            _Current_Config.Level++;
            _Current_Config.UpgradePrice = (int)Mathf.Round(UpgradePrice * 1.17f);
        }
    }

    public void Attack()
    {
        if (_Monster_Pool.CurrentTarget && Time.time > _Next_Fire)
        {
            StartCoroutine(Shoot(_Monster_Pool.CurrentTarget));
            _Next_Fire = 1f / FireRate + Time.time;
        }
    }

    
    
    private IEnumerator Shoot(GameObject _current_Target)
    {
        Quaternion BulletRotate()
        {
            Vector3 _bullet_direction = _current_Target.transform.position - transform.position;
            float _angle_Rotate = Mathf.Atan(_bullet_direction.y/_bullet_direction.x) * Mathf.Rad2Deg;
            Quaternion _start_Rotate = Quaternion.Euler(0,0,_angle_Rotate + 90);

            return _start_Rotate;
        }

        GameObject _bullet = Instantiate(_Bullet_Prefab, transform.position, BulletRotate());
        while (_current_Target && _bullet.transform.position != _current_Target.transform.position)
        {
            _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, 
                _current_Target.transform.position,
                _Bullet_Speed * Time.deltaTime);
            
            yield return null;
        }

        float _final_Damage;
        if (_Increase_Damage < 1)
            _Increase_Damage = 1;
        
        _final_Damage = Damage * _Increase_Damage;
        if (_current_Target)
        {
            _current_Target.GetComponent<IDamageable>().ApplyDamage(_final_Damage);
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

    private void OnDestroy()
    {
        GameEvents._Start_Level -= DestroyAlly;
    }
}
