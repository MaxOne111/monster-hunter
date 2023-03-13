using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerAlliesUI : MonoBehaviour
{
    [SerializeField] private Image _Icon;
    [SerializeField] private TextMeshProUGUI _Name;
    [SerializeField] private TextMeshProUGUI _Damage;
    
    
    private PlayerAllies _Allies;

    [Inject]
    private void Construct(PlayerAllies _allies)
    {
        _Allies = _allies;
    }
    

    public void AllyData()
    {
        if (_Allies.Allies.Count > 0)
        {
            for (int i = 0; i < _Allies.Allies.Count; i++)
            {
                _Icon.GetComponent<Image>().sprite = _Allies.AlliesOnScene[i].Icon;
                _Name.text = $"Name: {_Allies.AlliesOnScene[i].Name}";
                _Damage.text = $"Damage: {_Allies.AlliesOnScene[i].Damage}";
            }
        }
    }
}
