using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerWeaponUI : MonoBehaviour, IWeaponUIVisitor
{
    [SerializeField] private TextMeshProUGUI _Weapon_Level;
    [SerializeField] private TextMeshProUGUI _Weapon_Damage;
    [SerializeField] private TextMeshProUGUI _Next_Upgrade;
    
    [SerializeField] private Image _Weapon_Image;
    
    [SerializeField] private Transform _Weapon_Tabs;
    [SerializeField] private WeaponButton _Weapon_Button;
    
    [SerializeField] private Button _Upgrade_Button;
    
    private GameUI _Game_UI;
    private Purchase _Purchase_Script;
    private PlayerWeapon _Player_Weapon;
    public IUpgradeable UpgradeableWeapon { get; set; }
    public Weapon CurrentWeapon { get; set; }

    [Inject]
    private void Construct(PlayerWeapon _player_Weapon, Purchase _purchase, GameUI _game_UI)
    {
        _Player_Weapon = _player_Weapon;
        _Purchase_Script = _purchase;
        _Game_UI = _game_UI;
    }
    
    //----------Implemented methods----------
    public void ShowWeaponData(Weapon _weapon ,IUpgradeable _upgradeable)
    {
        _Weapon_Level.text = "Level: " + _weapon.Level;
        _Weapon_Damage.text = "Damage: " + _weapon.Damage;
        _Weapon_Image.GetComponent<Image>().sprite = _weapon.Icon;
        _Upgrade_Button.interactable = true;

        if (_weapon.WeaponLevels.Count > 1)
        {
            _Next_Upgrade.text =
                $"Update \n+{_weapon.WeaponLevels[1].Damage - _weapon.Damage} {_upgradeable.UpgradePrice}$";
        }
        else
        {
            _Next_Upgrade.text = "Max upgrade";
            _Upgrade_Button.interactable = false;
        }
    }

    public void ShowWeaponData(Weapon _weapon)
    {
        _Weapon_Level.text = "Level: " + _weapon.Level;
        _Weapon_Damage.text = "Damage: " + _weapon.Damage;
        _Weapon_Image.GetComponent<Image>().sprite = _weapon.Icon;
        _Upgrade_Button.interactable = false;
        _Next_Upgrade.text = "Not upgradeable";
    }
    
    //----------Class methods----------
    private void Awake() => CreateWeaponButtons();
    private void Start() => _Player_Weapon.CurrentWeapon.WeaponUIAccept(this);
    private void OnEnable() => _Upgrade_Button.onClick.AddListener(BuyUpgrade);

    private void ChangeWeapon(Weapon _weapon)
    {
        _Player_Weapon.ChangeWeapon(_weapon);
    }
    private void CreateWeaponButtons()
    {
        for (int i = 0; i < _Player_Weapon.Weapon.Count; i++)
        {
            WeaponButton _button = Instantiate(_Weapon_Button, _Weapon_Tabs);
            _button.Weapon = _Player_Weapon.Weapon[i];
            _button.WeaponData(_button.Weapon.Icon);
            _button.Button.onClick.AddListener(delegate
            {
                _button.Weapon.WeaponUIAccept(this);
                ChangeWeapon(_button.Weapon);
            });
        }
    }

    private void BuyUpgrade()
    {
        _Purchase_Script.Upgrade(UpgradeableWeapon);
        _Player_Weapon.CurrentWeapon.WeaponUIAccept(this);
        _Game_UI.CurrentCoins();
    }
    
}
