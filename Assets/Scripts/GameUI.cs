using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image[] _Health;
    [SerializeField] private TextMeshProUGUI _Combo_Text;
    [SerializeField] private TextMeshProUGUI _Monsters_Count;
    [SerializeField] private TextMeshProUGUI _Money;
    [SerializeField] private TextMeshProUGUI _Gems;
    [SerializeField] private GameObject _New_Game_Panel;

    private void Start()
    {
        CurrentCoins();
    }

    public void CurrentHealth(float _max_Health, float _current_Health)
    {
        for (int i = 0; i < _Health.Length; i++)
        {
            _Health[i].fillAmount = _current_Health / _max_Health;
        }
    }

    public void CurrentCombo(int _attack_Count)
    {
        _Combo_Text.text = _attack_Count + "X";
    }

    public void CurrentCoins()
    {
        _Money.text = PlayerData.Coins.ToString("0");
    }

    public void CurrentGems()
    {
        _Gems.text = PlayerData.Gems.ToString("0");
    }

    public void CurrentMonstersCount(MonstersPool _monsters)
    {
        int _killed_Monsters = _monsters.StartMonsterCount - _monsters.CurrentMonsterCount;
        _Monsters_Count.text = _killed_Monsters + "/" + _monsters.StartMonsterCount;
    }
    
    public void ComboTextSetActive(bool _is_Active)
    {
        if (_is_Active)
        {
            _Combo_Text.gameObject.SetActive(true);
        }
        else
        {
            _Combo_Text.gameObject.SetActive(false);
        }
            
    }

    public void NewGamePanel()
    {
        _New_Game_Panel.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
