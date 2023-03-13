using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAllies : MonoBehaviour
{
    [field: SerializeField] public List<Ally> Allies { get; private set; }
    private List<Ally> _Allies_On_Scene = new List<Ally>();
    [SerializeField] private Transform[] _Allies_Positions;
    [Inject] private DiContainer _Di_Container;
    
    public List<Ally> AlliesOnScene{get=>_Allies_On_Scene;}

    private void Awake()
    {
        GameEvents._Start_Level += ResetAllies;
        GameEvents._Start_Level += AllyOnScene;
    }

    public void AddAlly(Ally _ally)
    {
        Allies.Add(_ally);
    }
    
    private void AllyOnScene()
    {
        if (Allies.Count > 0)
        {
            for (int i = 0; i < Allies.Count; i++)
            {
                GameObject _ally = _Di_Container.InstantiatePrefab(Allies[i].gameObject, _Allies_Positions[i].position,
                    Quaternion.identity, null);
                _Allies_On_Scene.Add(_ally.GetComponent<Ally>());
            }
        }
    }

    private void ResetAllies()
    {
        if (_Allies_On_Scene.Count > 0)
        {
            for (int i = 0; i < _Allies_On_Scene.Count; i++)
            {
                _Allies_On_Scene.Remove(_Allies_On_Scene[i]);
            }
        }
    }

    private void OnDestroy()
    {
        GameEvents._Start_Level -= AllyOnScene;
        GameEvents._Start_Level -= ResetAllies;
    }
}
