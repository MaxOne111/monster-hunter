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
    [SerializeField] private TextMeshProUGUI _Level;
    [SerializeField] private TextMeshProUGUI _Damage;
    
    private PlayerAllies _Allies;
    private Purchase _Purchase_Script;
    private GameUI _Game_UI;

    [Inject]
    private void Construct(PlayerAllies _allies, Purchase _purchase, GameUI _game_UI)
    {
        _Allies = _allies;
        _Purchase_Script = _purchase;
        _Game_UI = _game_UI;
    }
    

    public void ShowAllyData()
    {
        if (_Allies.Allies.Count > 0)
        {
            _Icon.GetComponent<Image>().sprite = _Allies.AlliesOnScene[0].Icon;
            _Name.text = $"{_Allies.AlliesOnScene[0].Name}";
            _Level.text = $"Level: {_Allies.AlliesOnScene[0].Level}";
            _Damage.text = $"Damage: {_Allies.AlliesOnScene[0].Damage}";
        }
    }

    public void Upgrade()
    {
        _Purchase_Script.Upgrade(_Allies.Allies[0]);
        ShowAllyData();
        _Game_UI.CurrentCoins();
    }
}
