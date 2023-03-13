using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Configs/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public int LevelNumber { get; private set; }
    [field: SerializeField] public MonsterPoolConfig PoolConfig { get; private set; }

}
