using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Cheats : MonoBehaviour
{
    [SerializeField] private Level[] _Level;
    private GameUI _UI;
    private GameLevels _Game_Levels;
    
    [Inject]
    private void Construct(GameUI _ui, GameLevels _levels)
    {
        _UI = _ui;
        _Game_Levels = _levels;
    }
    
    public void GetMoney()
    {
        PlayerData.RecieveCoins(5000);
        _UI.CurrentCoins();
    }
    
    public void LoadCustomLevel(int _index)
    {
        _Game_Levels.SetLevelNumber(_index);
        if (_index < _Level.Length)
        {
            for (int i = 0; i < _Level.Length; i++)
            {
                _Level[i].gameObject.SetActive(false);
            }
            
            _Level[_index].gameObject.SetActive(true);
            GameEvents.StartLevel();
            _UI.CurrentLevel(_Game_Levels);
        }
    }
}
