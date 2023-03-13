using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerWeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Weapon_Level;
    [SerializeField] private TextMeshProUGUI _Weapon_Damage;
    [SerializeField] private TextMeshProUGUI _Next_Upgrade;
    [SerializeField] private Image _Weapon_Image;
    private GameUI _Game_UI;
    private Weapon _Selected_Weapon;
    
    private Purchase _Purchase_Script;
    private PlayerWeapon _Player_Weapon;
    

    [Inject]
    private void Construct(PlayerWeapon _player_Weapon, Purchase _purchase, GameUI _game_UI)
    {
        _Player_Weapon = _player_Weapon;
        _Purchase_Script = _purchase;
        _Game_UI = _game_UI;
    }

    public void SelectWeaponTab(int _weapon_Number)
    {
        if (_weapon_Number < _Player_Weapon.Weapon.Count && _Player_Weapon.Weapon[_weapon_Number])
        {
            _Selected_Weapon = _Player_Weapon.Weapon[_weapon_Number];
            _Player_Weapon.ChangeWeapon(_Selected_Weapon);
        }
    }

    public void ShowWeaponDetails()
    {
        if (!_Selected_Weapon)
        {
            _Selected_Weapon = _Player_Weapon.Weapon[0];
        }
        _Weapon_Level.text = "Level: " + _Selected_Weapon.Upgrades[0].Level;
        _Weapon_Damage.text = "Damage: " + _Selected_Weapon.Upgrades[0].Damage;
        _Weapon_Image.GetComponent<Image>().sprite = _Selected_Weapon.Upgrades[0].Icon;

        if (_Selected_Weapon.Upgrades.Count > 1)
        {
            _Next_Upgrade.text = "Update \n + " + (_Selected_Weapon.Upgrades[1].Damage - _Selected_Weapon.Damage) 
                + " " + _Selected_Weapon.Upgrades[0].UpgradePrice + "$";
        }
        else
        {
            _Next_Upgrade.text = "Max upgrade";
        }
        
    }

    public void BuyUpgrade()
    {
        _Purchase_Script.Buy(_Selected_Weapon);
        _Game_UI.CurrentCoins();
    }
}
