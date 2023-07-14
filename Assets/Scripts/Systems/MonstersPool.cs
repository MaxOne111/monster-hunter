using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonstersPool : MonoBehaviour
{
    [SerializeField] private GameLevels _Game_Levels;
    [HideInInspector]
    [SerializeField] private MonsterPoolConfig _Config;
    //[HideInInspector] 
    [SerializeField] private List<Monster> _Monsters;
    [SerializeField] private Monster _Boss;
    private float _Health_Ratio;

    [Header("Monsters start position")]
    [SerializeField] private Transform _Start_Position;
    [Space]
    private GameUI _Game_UI;
    [SerializeField] private ParticleSystem _Appearance_Effect;
    private DiContainer _Di_Container;
    public GameObject CurrentTarget { get; private set; }
    public int StartMonsterCount { get; private set; }

    public int CurrentMonsterCount
    {
        get
        {
            if (!_Boss)
                return _Monsters.Count;
            
            return _Monsters.Count + 1;
        }
    }

    [Inject]
    private void Construct(GameUI _ui, DiContainer _container)
    {
        _Di_Container = _container;
        _Game_UI = _ui;
    }
    
    private void Awake()
    {
        GameEvents._Start_Level += ResetPool;
        GameEvents._Monster_Death += NewMonster;
    }
    
    private void Start()
    {
        StartHealth();
    }
    
    private void StartSettings()
    {
        _Config = _Game_Levels.GetPoolConfig(_Config);
        for (int i = 0; i < _Config.Monsters.Count; i++)
        {
            _Monsters.Add(_Config.Monsters[i]);
        }
        _Boss = _Config.Boss;
        _Health_Ratio = _Config.HealthRatio;
    }

    private void StartHealth()
    {
        for (int i = 1; i < _Monsters.Count; i++)
        {
            _Monsters[i].StartHealth(_Monsters[i-1].MaxHealth, _Health_Ratio);
        }
        _Boss.StartHealth(_Monsters[0].MaxHealth, 5);
    }
    
    private void NewMonster()
    {
        _Monsters.Remove(_Monsters[0]);
        
        if(_Monsters.Count > 0)
            StartCoroutine(CreateMonster());
        
        else if (_Monsters.Count == 0 && _Boss)
        {
            _Monsters.Add(_Boss);
            _Boss = null;
            StartCoroutine(CreateMonster());
        }
        else
        {
            _Game_UI.NewGamePanel();
        }
        _Game_UI.CurrentMonstersCount(this);
        
    }

    private IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(1.5f);
        _Appearance_Effect.Play();
        CurrentTarget = _Di_Container.InstantiatePrefab(_Monsters[0].gameObject, _Start_Position.position, Quaternion.identity,null);
    }
    
    private void ResetPool()
    {
        _Monsters.Clear();
        StartSettings();
        StartMonsterCount = CurrentMonsterCount;
        StartHealth();

        CurrentTarget = _Di_Container.InstantiatePrefab(_Monsters[0].gameObject, _Start_Position.position, Quaternion.identity,null);
        _Appearance_Effect.Play();
        _Game_UI.CurrentMonstersCount(this);
    }

    private void OnDestroy()
    {
        GameEvents._Monster_Death -= NewMonster;
        GameEvents._Start_Level -= ResetPool;
    }
}
