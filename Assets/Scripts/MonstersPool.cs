using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonstersPool : MonoBehaviour
{
    [Header("Monsters on level")]
    [SerializeField] private List<Monster> _Monsters;
    [SerializeField] private Monster _Boss;
    [SerializeField] private float _Health_Ratio;
    [Header("Monsters start position")]
    [SerializeField] private Transform _Start_Position;
    [Space]
    [SerializeField] private ParticleSystem _Appearance_Effect;

    private GameUI _Game_UI;
    [Inject] private DiContainer _Di_Container;
    public GameObject CurrentMonster { get; private set; }
    public int StartMonsterCount { get; private set; }

    public int CurrentMonsterCount
    {
        get
        {
            if (_Boss)
            {
                int _monster_Count = _Monsters.Count + 1;
                return _monster_Count;
            }
            else
            {
                return _Monsters.Count;
            }
        }
    }

    [Inject]
    private void Construct(GameUI _ui)
    {
        _Game_UI = _ui;
    }
    
    private void Awake()
    {
        GameEvents._Monster_Death += NewMonster;
        StartMonsterCount = CurrentMonsterCount;
    }
    private void Start()
    {
        CurrentMonster = _Di_Container.InstantiatePrefab(_Monsters[0].gameObject, _Start_Position.position, Quaternion.identity,null);
        _Appearance_Effect.Play();
        _Game_UI.CurrentMonstersCount(this);
        
        StartHealth();
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
        CurrentMonster = _Di_Container.InstantiatePrefab(_Monsters[0].gameObject, _Start_Position.position, Quaternion.identity,null);
    }
    
    private void OnDisable()
    {
        GameEvents._Monster_Death -= NewMonster;
    }

    private void OnDestroy()
    {
        GameEvents._Monster_Death -= NewMonster;
    }
}
