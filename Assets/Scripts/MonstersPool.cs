using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonstersPool : MonoBehaviour
{
    [Header("Monsters on level")]
    [SerializeField] private List<GameObject> _Monsters;
    [SerializeField] private GameObject _Boss;
    [Header("Monsters start position")]
    [SerializeField] private Transform _Start_Position;
    [Space]
    [SerializeField] private ParticleSystem _Appearance_Effect;
    [Inject] private DiContainer _Di_Container;
    public GameObject CurrentMonster { get; private set; }
    private void Start()
    {
        CurrentMonster = _Di_Container.InstantiatePrefab(_Monsters[0], _Start_Position.position, Quaternion.identity,null);
        _Appearance_Effect.Play();
    }

    private void Awake()
    {
        GameEvents._Monster_Death += NewMonster;
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
        
            
    }

    private IEnumerator CreateMonster()
    {
        yield return new WaitForSeconds(3);
        _Appearance_Effect.Play();
        CurrentMonster = _Di_Container.InstantiatePrefab(_Monsters[0], _Start_Position.position, Quaternion.identity,null);
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
