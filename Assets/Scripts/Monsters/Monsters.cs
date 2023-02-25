using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Monsters : MonoBehaviour
{
    [SerializeField] private float _Max_Health;
    [SerializeField] private float _Regeneration_Speed;
    private bool _Is_Regenerating;
    private GameUI _Game_UI;
    public float CurrentHealth { get; private set; }
    
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
        GameEvents.MonsterDeath();
        Destroy(gameObject);
    }
}
