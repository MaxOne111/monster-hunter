using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] private float _Max_Health;
    [SerializeField] private float _Regeneration_Speed;
    [SerializeField] private int _Money_Reward;
    private bool _Is_Regenerating;
    private GameUI _Game_UI;
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get=>_Max_Health; }
    
    [Inject]
    private void Construct(GameUI _game_UI)
    {
        _Game_UI = _game_UI;
    }
    
    private void Awake()
    {
        CurrentHealth = _Max_Health;
    }

    private void Start()
    {
        _Game_UI.CurrentHealth(_Max_Health, CurrentHealth);
    }

    private IEnumerator HealthRegeneration()
    {
        _Is_Regenerating = true;
        while (CurrentHealth < _Max_Health)
        {
            CurrentHealth += (_Regeneration_Speed / _Max_Health) * Time.deltaTime;
            _Game_UI.CurrentHealth(_Max_Health, CurrentHealth);
            yield return null;
        }
        _Is_Regenerating = false;
    }

    public void StartHealth(float _health, float _ratio)
    {
        _Max_Health = _health * _ratio;
        _Max_Health = Mathf.Round(_Max_Health * 100) * 0.01f;
    }

    protected virtual void Reward()
    {
        PlayerData.RecieveCoins(_Money_Reward);
        _Game_UI.CurrentCoins();
    }

    public void ApplyDamage(float _damage)
    {
        if (CurrentHealth - _damage > 0)
        {
            CurrentHealth -= _damage;
            if(!_Is_Regenerating)
                StartCoroutine(HealthRegeneration());
        }
        else
        {
            Death();
        }
    }

    private void Death()
    {
        CurrentHealth = 0;
        _Game_UI.CurrentHealth(_Max_Health,0);
        GameEvents.MonsterDeath();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Reward();
    }
}
