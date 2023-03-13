using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public LevelConfig LevelsConfig { get; private set; }
    public int LevelNumber { get; private set; }

    private void Awake()
    {
        LevelNumber = LevelsConfig.LevelNumber;
    }
}
