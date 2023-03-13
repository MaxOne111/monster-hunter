using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameLevels : MonoBehaviour
{
    [field: SerializeField] public Level[] Levels { get; private set; }
    [field: SerializeField] public int LevelNumber { get; private set; }

    private GameUI _Game_UI;

    [Inject]
    private void Construct(GameUI _ui)
    {
        _Game_UI = _ui;
    }

    private void Awake()
    {
        LoadStartLevel();
    }

    private void LoadStartLevel()
    {
        if (LevelNumber < Levels.Length)
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                Levels[i].gameObject.SetActive(false);
            }
            
            Levels[LevelNumber].gameObject.SetActive(true);
            GameEvents.StartLevel();
            _Game_UI.CurrentLevel(this);
        }
    }

    public void SetLevelNumber(int _value)
    {
        LevelNumber = _value;
    }

    public MonsterPoolConfig GetPoolConfig(MonsterPoolConfig _config)
    {
        _config = Levels[LevelNumber].LevelsConfig.PoolConfig;
        return _config;
    }
}
