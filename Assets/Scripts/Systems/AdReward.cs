using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

public class AdReward : MonoBehaviour
{
    [SerializeField] private int _Reward_Gems = 5;
    private GameUI _Game_UI;
    public LevelGrid LevelGrid { get; set; }
    public int LockedTileIndex { get; set; }
    
    [Inject]
    private void Construct(GameUI _ui)
    {
        _Game_UI = _ui;
    }

    public void RemoveObstacle()
    {
        LevelGrid.UnlockCustomTile(LockedTileIndex);
    }

    public void AdGems()
    {
        PlayerData.RecieveGems(_Reward_Gems);
        _Game_UI.CurrentGems();
    }

    public void CurrentGems()
    {
        _Game_UI.CurrentGems();
    }
}
